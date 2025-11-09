using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL.Utilidades
{
    public class OracleDynamicParameters : SqlMapper.IDynamicParameters
    {
        private readonly DynamicParameters _dynamicParameters = new DynamicParameters();
        private readonly List<OracleParameter> _oracleParameters = new List<OracleParameter>();

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
                    // Crear una copia del parámetro para evitar el error "already contained"
                    var newParam = new OracleParameter(param.ParameterName, param.OracleDbType)
                    {
                        Direction = param.Direction,
                        Value = param.Value,
                        Size = param.Size
                    };

                    oracleCommand.Parameters.Add(newParam);
                }
            }
        }

        public T Get<T>(string parameterName)
        {
            var param = _oracleParameters.FirstOrDefault(p => p.ParameterName == parameterName);

            if (param?.Value == null || param.Value == DBNull.Value)
            {
                return default(T);
            }

            // Manejar OracleDecimal para convertir a int
            if (typeof(T) == typeof(int) && param.Value is Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal)
            {
                return (T)(object)oracleDecimal.ToInt32();
            }

            return (T)param.Value;
        }
    }
}