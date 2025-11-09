using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DATA
{
    public class OracleDbContext
    {
        private readonly string _connectionString;

        public OracleDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}
