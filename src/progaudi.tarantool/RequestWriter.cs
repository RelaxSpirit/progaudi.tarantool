using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProGaudi.Tarantool.Client.Model;

namespace ProGaudi.Tarantool.Client
{
    internal class RequestWriter : IRequestWriter
    {
        private readonly ClientOptions _clientOptions = null;
        private readonly IPhysicalConnection _physicalConnection = null;
        private readonly Queue<ArraySegment<byte>> _buffer = new();
        private readonly object _lock = new();
        private readonly Thread _thread = null;
        private readonly ManualResetEventSlim _exitEvent = new();
        private readonly ManualResetEventSlim _newRequestsAvailable = new();
        private readonly ConnectionOptions _connectionOptions = null;
        private bool _disposed = false;
        private long _remaining = 0;

        public RequestWriter(ClientOptions clientOptions, IPhysicalConnection physicalConnection)
        {
            _clientOptions = clientOptions;
            _physicalConnection = physicalConnection;
            _thread = new Thread(WriteFunction)
            {
                IsBackground = true,
                Name = $"{clientOptions.Name} :: Write"
            };
            _connectionOptions = _clientOptions.ConnectionOptions;
        }

        public void BeginWriting()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ResponseReader));
            }

            _clientOptions?.LogWriter?.WriteLine("Starting thread");
            _thread.Start();
        }

        public bool IsConnected => !_disposed;

        public void Write(ArraySegment<byte> request)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ResponseReader));
            }

            _clientOptions?.LogWriter?.WriteLine($"Enqueuing request: {request.Count} bytes.");
            bool shouldSignal = false;
            lock (_lock)
            {
                _buffer.Enqueue(request);
                shouldSignal = _buffer.Count == 1;
            }

            if (shouldSignal)
                _newRequestsAvailable.Set();
        }

        public void Dispose()
        {
            if (_exitEvent.IsSet || _disposed)
            {
                return;
            }

            _disposed = true;
            _exitEvent.Set();
            _thread.Join();
            _exitEvent.Dispose();
            _newRequestsAvailable.Dispose();
        }

        private void WriteFunction()
        {
            var handles = new[] { _exitEvent.WaitHandle, _newRequestsAvailable.WaitHandle };
            var throttle = _connectionOptions.WriteThrottlePeriodInMs;
            while (true)
            {
                switch (WaitHandle.WaitAny(handles))
                {
                    case 0:
                        return;
                    case 1:
                        WriteRequests(_connectionOptions.WriteStreamBufferSize,
                            _connectionOptions.MaxRequestsInBatch);

                        long remaining = Interlocked.Read(ref _remaining);

                        // Thread.Sleep will be called only if the number of pending bytes less than
                        // MinRequestsWithThrottle

                        if (throttle > 0 && remaining < _connectionOptions.MinRequestsWithThrottle)
                            Thread.Sleep(throttle);

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void WriteRequests(int bufferLength, int limit)
        {
            void WriteBuffer(ArraySegment<byte> buffer)
            {
                _physicalConnection.Write(buffer.Array, buffer.Offset, buffer.Count);
            }

            bool GetRequest(out ArraySegment<byte> result)
            {
                lock (_lock)
                {
                    if (_buffer.Count > 0)
                    {
                        _remaining = _buffer.Count + 1;
                        result = _buffer.Dequeue();
                        return true;
                    }
                }

                result = default;
                return false;
             }

            var count = 0;
            UInt64 length = 0;
            var list = new List<ArraySegment<byte>>();
            while (GetRequest(out ArraySegment<byte> request))
            {
                _clientOptions?.LogWriter?.WriteLine($"Writing request: {request.Count} bytes.");
                length += (uint)request.Count;

                list.Add(request);
                _clientOptions?.LogWriter?.WriteLine($"Wrote request: {request.Count} bytes.");

                count++;
                if ((limit > 0 && count > limit) || length > (ulong)bufferLength)
                {
                    break;
                }

            }

            if (list.Any())
            {
                // merge requests into one buffer
                var result = new byte[length];
                int position = 0;
                foreach (var r in list)
                {
                    Buffer.BlockCopy(r.Array, r.Offset, result, position, r.Count);
                    position += r.Count;
                }

                WriteBuffer(new ArraySegment<byte>(result));
            }

            lock (_lock)
            {
                if (_buffer.Count == 0)
                    _newRequestsAvailable.Reset();
            }

            _physicalConnection.Flush();
        }
    }

    internal class RequestWriterAsync : IRequestWriter
    {
        private readonly ClientOptions _clientOptions = null;
        private readonly IPhysicalConnection _physicalConnection = null;
        private readonly ConcurrentQueue<ArraySegment<byte>> _buffer = new();
        private readonly ManualResetEventSlim _exitEvent = new ();
        private readonly ManualResetEventSlim _newRequestsAvailable = new();
        private readonly ConnectionOptions _connectionOptions = null;
        private readonly CancellationTokenSource cancellationTokenSource = new();
        private readonly Task _writerTask;
        private bool _disposed = false;
        private long _remaining = 0;

        public RequestWriterAsync(ClientOptions clientOptions, IPhysicalConnection physicalConnection)
        {
            _clientOptions = clientOptions;
            _physicalConnection = physicalConnection;

            _writerTask = Task.Factory.StartNew(async () => await WriteFunction(), cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            _connectionOptions = _clientOptions.ConnectionOptions;
        }

        public void BeginWriting()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ResponseReader));
            }

            //_clientOptions?.LogWriter?.WriteLine("Starting thread");
            //_thread.Start();
        }

        public bool IsConnected => !_disposed;

        public void Write(ArraySegment<byte> request)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ResponseReader));
            }

            _clientOptions?.LogWriter?.WriteLine($"Enqueuing request: {request.Count} bytes.");
            _buffer.Enqueue(request);
           
            if (_buffer.Count == 1)
                _newRequestsAvailable.Set();
        }

        public void Dispose()
        {
            if (_exitEvent.IsSet || _disposed)
            {
                return;
            }

            _disposed = true;
            if (!cancellationTokenSource.IsCancellationRequested)
                cancellationTokenSource.Cancel();
            _exitEvent.Set();
            _exitEvent.Dispose();
            _newRequestsAvailable.Dispose();
            
            cancellationTokenSource.Dispose();

            _writerTask.Wait();
            _writerTask.Dispose();
        }

        private async Task WriteFunction()
        {
            _clientOptions?.LogWriter?.WriteLine($"Starting thread {_clientOptions.Name} :: Write");

            var handles = new[] { _exitEvent.WaitHandle, _newRequestsAvailable.WaitHandle };
            var throttle = _connectionOptions.WriteThrottlePeriodInMs;
            long remaining;
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                switch (WaitHandle.WaitAny(handles))
                {
                    case 0:
                        return;
                    case 1:
                        await WriteRequests(_connectionOptions.WriteStreamBufferSize,
                            _connectionOptions.MaxRequestsInBatch);

                        remaining = Interlocked.Read(ref _remaining);

                        // Thread.Sleep will be called only if the number of pending bytes less than
                        // MinRequestsWithThrottle

                        if (throttle > 0 && remaining < _connectionOptions.MinRequestsWithThrottle)
                            await Task.Delay(throttle, cancellationTokenSource.Token);

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private async Task WriteRequests(int bufferLength, int limit)
        {
            bool GetRequest(out ArraySegment<byte> result)
            {
                if (!_buffer.IsEmpty)
                    Interlocked.Exchange(ref _remaining, _buffer.Count + 1);

                return _buffer.TryDequeue(out result);
            }

            var count = 0;
            UInt64 length = 0;
            var list = new List<ArraySegment<byte>>();
            while (GetRequest(out ArraySegment<byte> request))
            {
                _clientOptions?.LogWriter?.WriteLine($"Writing request: {request.Count} bytes.");
                length += (uint)request.Count;

                list.Add(request);
                _clientOptions?.LogWriter?.WriteLine($"Wrote request: {request.Count} bytes.");

                count++;
                if ((limit > 0 && count > limit) || length > (ulong)bufferLength)
                {
                    break;
                }

            }

            if (list.Any())
            {
                // merge requests into one buffer
                var result = new byte[length];
                int position = 0;
                foreach (var r in list)
                {
                    Buffer.BlockCopy(r.Array, r.Offset, result, position, r.Count);
                    position += r.Count;
                }

                await _physicalConnection.WriteAsync(result, 0, position);
            }

            if (_buffer.IsEmpty)
                _newRequestsAvailable.Reset();

            await _physicalConnection.Flush();
        }
    }
}
