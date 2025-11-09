using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Utilidades
{
    public class OracleDynamicParameters : SqlMapper.IDynamicParameters
    {
        private readonly DynamicParameters _dynamicParameters = new DynamicParameters();
        private readonly OracleParameterCollection _oracleParameters = new OracleCommand().Parameters;

        public void Add(string name, object value = null, OracleDbType? dbType = null, ParameterDirection? direction = null, int? size = null)
        {
            OracleParameter param;

            if (dbType.HasValue)
            {
                param = new OracleParameter(name, dbType.Value, direction ?? ParameterDirection.Input);
            }
            else
            {
                param = new OracleParameter(name, value);
            }

            if (direction.HasValue)
            {
                param.Direction = direction.Value;
            }

            if (size.HasValue)
            {
                param.Size = size.Value;
            }

            if (value != null && direction != ParameterDirection.Output && direction != ParameterDirection.ReturnValue)
            {
                param.Value = value;
            }

            _oracleParameters.Add(param);
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            if (command is OracleCommand oracleCommand)
            {
                oracleCommand.Parameters.Clear();

                foreach (OracleParameter param in _oracleParameters)
                {
                    oracleCommand.Parameters.Add(param);
                }
            }
        }

        public T Get<T>(string parameterName)
        {
            var param = _oracleParameters[parameterName];
            if (param?.Value == null || param.Value == DBNull.Value)
            {
                return default(T);
            }

            return (T)param.Value;
        }
    }
}
