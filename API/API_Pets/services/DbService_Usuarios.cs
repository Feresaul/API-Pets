using API_Pets.models;
using DemoApiUsers.models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace API_Pets.services
{
    public class DbService_Usuarios : IDbService_Usuarios
    {
        private SqlConnection _connection;

        public DbService_Usuarios()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connString = config.GetConnectionString("Default");

            _connection = new SqlConnection(connString);
        }

        public async Task<ResponseBase<int>> logIn(Usuario usuario)
        {
            var param_usuario = new SqlParameter("@usuario", System.Data.SqlDbType.NVarChar,50);
            param_usuario.Value = usuario.usuario;

            var contrasenia = new Helper().SHA1(usuario.contrasenia);

            var param_contrasenia = new SqlParameter("@contrasenia", System.Data.SqlDbType.NVarChar, -1);
            param_contrasenia.Value = contrasenia;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@mensaje", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;
            var param_id = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
            param_id.Direction = System.Data.ParameterDirection.Output;


            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_login";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_usuario);
                    comando.Parameters.Add(param_contrasenia);
                    comando.Parameters.Add(param_id);
                    comando.Parameters.Add(param_error);
                    comando.Parameters.Add(param_mensaje);
                    var reader = await comando.ExecuteNonQueryAsync();

                }
            }
            catch
            {
                return new ResponseBase<int> { TieneError = true, Mensaje = "Error interno. Consulte al administrador del sistema.", Modelo = -1 };

            }
            finally
            {
                _connection.Close();
            }
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = (int)param_id.Value };
        }

        public async Task<ResponseBase<int>> registro(Usuario usuario)
        {
            var param_usuario = new SqlParameter("@usuario", System.Data.SqlDbType.NVarChar, 50);
            param_usuario.Value = usuario.usuario;
            var param_nombre = new SqlParameter("@nombre", System.Data.SqlDbType.NVarChar, 100);
            param_nombre.Value = usuario.nombreCompleto;
            var param_correo = new SqlParameter("@correo", System.Data.SqlDbType.NVarChar, 100);
            param_correo.Value = usuario.email;
            var param_tel = new SqlParameter("@tel", System.Data.SqlDbType.NVarChar, 50);
            param_tel.Value = usuario.telefono;

            var contrasenia = new Helper().SHA1(usuario.contrasenia);

            var param_contrasenia = new SqlParameter("@contrasenia", System.Data.SqlDbType.NVarChar, -1);
            param_contrasenia.Value = contrasenia;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@mensaje", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;
            var param_id = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
            param_id.Direction = System.Data.ParameterDirection.Output;


            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_registro";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_usuario);
                    comando.Parameters.Add(param_nombre);
                    comando.Parameters.Add(param_correo);
                    comando.Parameters.Add(param_tel);
                    comando.Parameters.Add(param_contrasenia);
                    comando.Parameters.Add(param_id);
                    comando.Parameters.Add(param_error);
                    comando.Parameters.Add(param_mensaje);
                    var reader = await comando.ExecuteNonQueryAsync();

                }
            }
            catch
            {
                return new ResponseBase<int> { TieneError = true, Mensaje = "Error interno. Consulte al administrador del sistema.", Modelo = -1 };

            }
            finally
            {
                _connection.Close();
            }
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = (int)param_id.Value };
        }
    }
}
