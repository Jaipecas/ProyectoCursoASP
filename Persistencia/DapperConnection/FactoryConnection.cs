
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Persistencia.DapperConnection
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;
        private readonly IOptions<ConnectionConf> _options;

        public FactoryConnection(IOptions<ConnectionConf> options)
        {
            _options = options;
        }
        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetDbConnection()
        {
            _connection ??= new SqlConnection(_options.Value.DefaultConnection);

            if (_connection.State != ConnectionState.Open) _connection.Open();
          
            return _connection;
        }
    }
}
