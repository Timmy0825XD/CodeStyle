using DAL.Interfaces;
using DAL.Utilidades;
using Dapper;
using ENTITY.Usuarios;
using ENTITY.Utilidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaciones
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private readonly string _connectionString;

        public UsuarioDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<LoginResponseDTO>> Login(LoginRequestDTO loginRequest)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("p_correo", loginRequest.Correo, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_contrasena", loginRequest.Contrasena, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("v_cursor", dbType: OracleDbType.RefCursor, direction: ParameterDirection.ReturnValue);

                    var result = await connection.QueryAsync<LoginResponseDTO>(
                        "pkg_usuarios.login_usuario",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var usuario = result.FirstOrDefault();

                    if (usuario != null)
                    {
                        // Construir nombre completo
                        usuario.NombreCompleto = $"{usuario.NombreCompleto}"; // Ya viene del query
                        return Response<LoginResponseDTO>.Done("Login exitoso", usuario);
                    }
                    else
                    {
                        return Response<LoginResponseDTO>.Fail("Credenciales inválidas");
                    }
                }
            }
            catch (OracleException ex)
            {
                return Response<LoginResponseDTO>.Fail($"Error en Oracle: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<LoginResponseDTO>.Fail($"Error inesperado: {ex.Message}");
            }
        }

        public async Task<Response<int>> CrearUsuario(UsuarioDTO usuario)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("p_id_direccion", usuario.DireccionId, OracleDbType.Int32, ParameterDirection.Input);
                    parameters.Add("p_cedula", usuario.Cedula, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_primer_nombre", usuario.PrimerNombre, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_segundo_nombre", usuario.SegundoNombre, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_apellido_paterno", usuario.ApellidoPaterno, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_apellido_materno", usuario.ApellidoMaterno, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_telefono_principal", usuario.TelefonoPrincipal, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_telefono_secundario", usuario.TelefonoSecundario, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_correo", usuario.Correo, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_contrasena", usuario.Contrasena, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("v_id", dbType: OracleDbType.Int32, direction: ParameterDirection.ReturnValue);

                    await connection.ExecuteAsync(
                        "pkg_usuarios.crear_usuario",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    int idGenerado = parameters.Get<int>("v_id");

                    return Response<int>.Done("Usuario creado exitosamente", idGenerado);
                }
            }
            catch (OracleException ex)
            {
                // Capturar errores específicos del paquete
                if (ex.Message.Contains("ORA-20002"))
                {
                    return Response<int>.Fail("El correo o la cédula ya existen");
                }
                return Response<int>.Fail($"Error en Oracle: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<int>.Fail($"Error inesperado: {ex.Message}");
            }
        }

        public async Task<Response<bool>> ActualizarUsuario(UsuarioDTO usuario)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("p_id_usuario", usuario.Id, OracleDbType.Int32, ParameterDirection.Input);
                    parameters.Add("p_id_direccion", usuario.DireccionId, OracleDbType.Int32, ParameterDirection.Input);
                    parameters.Add("p_cedula", usuario.Cedula, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_primer_nombre", usuario.PrimerNombre, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_segundo_nombre", usuario.SegundoNombre, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_apellido_paterno", usuario.ApellidoPaterno, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_apellido_materno", usuario.ApellidoMaterno, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_telefono_principal", usuario.TelefonoPrincipal, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_telefono_secundario", usuario.TelefonoSecundario, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_correo", usuario.Correo, OracleDbType.Varchar2, ParameterDirection.Input);
                    parameters.Add("p_contrasena", usuario.Contrasena, OracleDbType.Varchar2, ParameterDirection.Input);

                    await connection.ExecuteAsync(
                        "pkg_usuarios.actualizar_usuario",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return Response<bool>.Done("Usuario actualizado exitosamente", true);
                }
            }
            catch (OracleException ex)
            {
                if (ex.Message.Contains("ORA-20004"))
                {
                    return Response<bool>.Fail("No se encontró el usuario a actualizar");
                }
                if (ex.Message.Contains("ORA-20005"))
                {
                    return Response<bool>.Fail("Correo o cédula duplicada");
                }
                return Response<bool>.Fail($"Error en Oracle: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail($"Error inesperado: {ex.Message}");
            }
        }

        public async Task<Response<UsuarioDTO>> ObtenerUsuarios()
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_cursor", dbType: OracleDbType.RefCursor, direction: ParameterDirection.ReturnValue);

                    var usuarios = await connection.QueryAsync<UsuarioListaDTO>(
                        "pkg_usuarios.listar_usuarios_activos",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    // Mapear a UsuarioDTO
                    var usuariosDTO = usuarios.Select(u => new UsuarioDTO
                    {
                        Id = u.IdUsuario,
                        PrimerNombre = u.PrimerNombre,
                        ApellidoPaterno = u.ApellidoPaterno,
                        Correo = u.Correo,
                        TelefonoPrincipal = u.TelefonoPrincipal,
                        FechaRegistro = u.FechaRegistro
                    }).ToList();

                    return Response<UsuarioDTO>.Done("Usuarios obtenidos exitosamente", list: usuariosDTO);
                }
            }
            catch (Exception ex)
            {
                return Response<UsuarioDTO>.Fail($"Error al obtener usuarios: {ex.Message}");
            }
        }

        public async Task<Response<bool>> EliminarUsuario(int idUsuario)
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("p_id_usuario", idUsuario, OracleDbType.Int32, ParameterDirection.Input);

                    await connection.ExecuteAsync(
                        "pkg_usuarios.eliminar_usuario",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return Response<bool>.Done("Usuario eliminado exitosamente", true);
                }
            }
            catch (OracleException ex)
            {
                if (ex.Message.Contains("ORA-20007"))
                {
                    return Response<bool>.Fail("No se encontró el usuario a eliminar");
                }
                return Response<bool>.Fail($"Error en Oracle: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail($"Error inesperado: {ex.Message}");
            }
        }
    }

    // DTO auxiliar para mapear el resultado del listar_usuarios_activos
    internal class UsuarioListaDTO
    {
        public int IdUsuario { get; set; }
        public string PrimerNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Correo { get; set; }
        public string TelefonoPrincipal { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string DireccionCompleta { get; set; }
        public string Ciudad { get; set; }
        public string Rol { get; set; }
    }
}
