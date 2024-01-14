using System.Collections.Generic;
using ProGaudi.Tarantool.Client.Model.Enums;

namespace ProGaudi.Tarantool.Client.Model.Requests
{
    public class ExecuteSqlRequest : IRequest
    {
        private static readonly SqlParameter[] Empty = System.Array.Empty<SqlParameter>();

        public ExecuteSqlRequest(string query, IReadOnlyList<SqlParameter> parameters)
        {
            Query = query;
            Parameters = parameters ?? Empty;
        }

        public string Query { get; }

        public IReadOnlyList<SqlParameter> Parameters { get; }

        public CommandCode Code => CommandCode.Execute;
    }
}