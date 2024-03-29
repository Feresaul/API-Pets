﻿using API.models;
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

        public async Task<ResponseBase<Usuario>> getUser(int id)
        {
            var param_idusuario = new SqlParameter("@id", System.Data.SqlDbType.Int);
            param_idusuario.Value = id;

            var usuario = new Usuario();

            try
            {
                _connection.Open();

                if(_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = $"mt_get_user";
                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(param_idusuario);

                    var reader = await command.ExecuteReaderAsync();

                    while(reader.Read())
                    {
                        usuario.id = id;
                        usuario.nombreCompleto = reader["nombre"].ToString();
                        usuario.usuario = reader["usuario"].ToString();
                        usuario.email = reader["correo"].ToString();
                        usuario.telefono = reader["telefono"].ToString();
                        usuario.idRol = int.Parse(reader["idRol"].ToString());
                        usuario.rol = reader["rol"].ToString();
                    }
                }

                return new ResponseBase<Usuario> { TieneError = false, Mensaje = "User obtained correctly", Modelo = usuario };
            }
            catch
            {
                return new ResponseBase<Usuario> { TieneError = true, Mensaje = "Error interno. Consulte al administrador del sistema.", Modelo = null };
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<ResponseBase<IEnumerable<Usuario>>> getUsers(int id)
        {
            try
            {
                var userList = new List<Usuario>();
                _connection.Open();

                if(_connection.State == System.Data.ConnectionState.Open)
                {
                    var param_id = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    param_id.Value = id;

                    string sql = "mt_get_users";
                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(param_id);

                    var reader = await command.ExecuteReaderAsync();

                    while(reader.Read())
                    {
                        var user = new Usuario();
                        user.id = int.Parse(reader["idUsuario"].ToString());
                        user.nombreCompleto = reader["nombre"].ToString();
                        user.usuario = reader["usuario"].ToString();
                        user.email = reader["correo"].ToString();
                        user.telefono = reader["telefono"].ToString();
                        user.idRol = int.Parse(reader["idRol"].ToString());
                        user.rol = reader["rol"].ToString();
                        userList.Add(user);
                    }
                }

                return new ResponseBase<IEnumerable<Usuario>> { TieneError = false, Mensaje = "User list obtained correctly.", Modelo = userList };
            } catch
            {
                return new ResponseBase<IEnumerable<Usuario>> { TieneError = true, Mensaje = "Error while obtaining user list.", Modelo = null };
            } finally
            {
                _connection.Close();
            }
        }

        public async Task<ResponseBase<int>> deleteUser(int id)
        {
            var param_id = new SqlParameter("@id", System.Data.SqlDbType.Int);
            param_id.Value = id;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_delete_user";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_id);
                    comando.Parameters.Add(param_error);
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
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = "User deleted successfully", Modelo = Convert.ToInt32(!(bool)param_error.Value) };

        }

        public async Task<ResponseBase<int>> updateUser(Usuario user)
        {
            var param_id = new SqlParameter("@id", System.Data.SqlDbType.Int);
            param_id.Value = user.id;

            var param_idRol = new SqlParameter("@idRol", System.Data.SqlDbType.Int);
            param_idRol.Value = user.idRol;

            var param_name = new SqlParameter("@name", System.Data.SqlDbType.NVarChar, -1);
            param_name.Value = user.nombreCompleto;

            var param_user = new SqlParameter("@user", System.Data.SqlDbType.NVarChar, -1);
            param_user.Value = user.usuario;

            var param_email = new SqlParameter("@email", System.Data.SqlDbType.NVarChar, -1);
            param_email.Value = user.email;

            var param_phone = new SqlParameter("@phone", System.Data.SqlDbType.NVarChar, -1);
            param_phone.Value = user.telefono;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@message", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_update_user";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_name);
                    comando.Parameters.Add(param_email);
                    comando.Parameters.Add(param_phone);
                    comando.Parameters.Add(param_user);
                    comando.Parameters.Add(param_id);
                    comando.Parameters.Add(param_idRol);
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
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = user.id };
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

        public async Task<ResponseBase<int>> registerEmployee(Usuario usuario)
        {
            var param_usuario = new SqlParameter("@usuario", System.Data.SqlDbType.NVarChar, 50);
            param_usuario.Value = usuario.usuario;
            var param_idRol = new SqlParameter("@idRol", System.Data.SqlDbType.Int);
            param_idRol.Value = usuario.idRol;
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
                    string sql = "mt_register_employee";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_usuario);
                    comando.Parameters.Add(param_idRol);
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
