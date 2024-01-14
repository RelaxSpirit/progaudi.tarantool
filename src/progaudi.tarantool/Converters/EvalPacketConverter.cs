﻿using System;

using ProGaudi.MsgPack.Light;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.Requests;

namespace ProGaudi.Tarantool.Client.Converters
{
    internal class EvalPacketConverter<T> : IMsgPackConverter<EvalRequest<T>>
    {
        private IMsgPackConverter<Key> _keyConverter;
        private IMsgPackConverter<string> _stringConverter;
        private IMsgPackConverter<T> _tupleConverter;

        public void Initialize(MsgPackContext context)
        {
            _keyConverter = context.GetConverter<Key>();
            _stringConverter = context.GetConverter<string>();
            _tupleConverter = context.GetConverter<T>();
        }

        public void Write(EvalRequest<T> value, IMsgPackWriter writer)
        {
            writer.WriteMapHeader(2);

            _keyConverter.Write(Key.Expression, writer);
            _stringConverter.Write(value.Expression, writer);

            _keyConverter.Write(Key.Tuple, writer);
            _tupleConverter.Write(value.Tuple, writer);
        }

        public EvalRequest<T> Read(IMsgPackReader reader)
        {
            throw new NotImplementedException();
        }
    }
}