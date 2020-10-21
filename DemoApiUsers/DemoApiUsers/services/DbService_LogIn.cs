using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoApiUsers.models;
using Microsoft.Extensions.Configuration;

namespace DemoApiUsers.services
{
    public class DbService_LogIn : IDbService_LogIn
    {
        private SqlConnection _connection;

        public DbService_LogIn()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connString = config.GetConnectionString("logIn");

            _connection = new SqlConnection(connString);
        }

        public async Task<ResponseBase<Usuario_LogIn>> RegisterUser(Usuario_LogIn modelo)
        {
            try
            {
                _connection.Open();

                /*if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = $"";

                    sql = $"insert into users values ('{modelo.username}','{modelo.password}'); select scope_identity();";

                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.Text;

                    var reader = await command.ExecuteScalarAsync();

                    modelo.id = int.Parse(reader.ToString());

                }*/

                SqlCommand cmd = new SqlCommand("registerUser", _connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", modelo.username);
                cmd.Parameters.AddWithValue("@password", modelo.password);
                modelo.id = await cmd.ExecuteNonQueryAsync();

                return new ResponseBase<Usuario_LogIn> { TieneResultado = true, Mensaje = "Usuario registrado correctamente", Modelo = modelo };

            }
            catch
            {
                return new ResponseBase<Usuario_LogIn> { TieneResultado = false, Mensaje = "Error interno. Consulte al administrador del sistema.", Modelo = null };

            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
