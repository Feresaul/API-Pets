using DemoApiUsers.models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApiUsers.services
{
    public class DbService_Services : IDbService_Services
    {
        private SqlConnection _connection;

        public DbService_Services()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connString = config.GetConnectionString("Default");

            _connection = new SqlConnection(connString);
        }

        public async Task<ResponseBase<int>> CancelarCita(int idCita)
        {
            var param_idCita = new SqlParameter("@idCita", System.Data.SqlDbType.Int);
            param_idCita.Value = idCita;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@mensaje", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_cancelar_cita";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_idCita);
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
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = idCita };
        }

        public async Task<ResponseBase<IEnumerable<ServicioCita>>> ObtenerHistorial(int idUsuario)
        {
            var param_idUsuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
            param_idUsuario.Value = idUsuario;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@mensaje", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            var listaServicios = new List<ServicioCita>();

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_historial_citas";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_idUsuario);
                    comando.Parameters.Add(param_error);
                    comando.Parameters.Add(param_mensaje);
                    var reader = await comando.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var servicio = new ServicioCita();
                        servicio.nombre = reader["nombre"].ToString();
                        servicio.precio = float.Parse(reader["precio"].ToString());
                        servicio.tiempo = int.Parse(reader["tiempoServicio"].ToString());

                        servicio.tipoServicio = reader["tipo"].ToString();
                        servicio.status = reader["status"].ToString();
                        servicio.fechaInicio = reader["fechaEntrada"].ToString();
                        servicio.fechaTermino = reader["fechaSalida"].ToString();
                        listaServicios.Add(servicio);
                    }
                }
            }
            catch
            {
                return new ResponseBase<IEnumerable<ServicioCita>> { TieneError = true, Mensaje = "Error interno. Consulte al administrador del sistema.", Modelo = null };

            }
            finally
            {
                _connection.Close();
            }
            return new ResponseBase<IEnumerable<ServicioCita>> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = listaServicios };
        }

        public async Task<ResponseBase<IEnumerable<Servicio>>> ObtenerPorTipo(string tipo)
        {
            var param_tipo = new SqlParameter("@tipoServicio", System.Data.SqlDbType.NVarChar, 50);
            param_tipo.Value = tipo; 

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@mensaje", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            var listaServicios = new List<Servicio>();

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_obtener_servicios_tipo";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_tipo);
                    comando.Parameters.Add(param_error);
                    comando.Parameters.Add(param_mensaje);
                    var reader = await comando.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var servicio = new Servicio();
                        servicio.id = int.Parse(reader["idServicio"].ToString());
                        servicio.nombre = reader["nombre"].ToString();
                        servicio.precio = float.Parse(reader["precio"].ToString());
                        servicio.tiempo = int.Parse(reader["tiempoServicio"].ToString());
                        listaServicios.Add(servicio);
                    }
                }
            }
            catch
            {
                return new ResponseBase<IEnumerable<Servicio>> { TieneError = true, Mensaje = "Error interno. Consulte al administrador del sistema.", Modelo = null };

            }
            finally
            {
                _connection.Close();
            }
            return new ResponseBase<IEnumerable<Servicio>> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = listaServicios };
        }

        public async Task<ResponseBase<IEnumerable<ServicioCita>>> ObtenerCitasPendientes(int idUsuario)
        {
            var param_idUsuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
            param_idUsuario.Value = idUsuario;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@mensaje", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            var listaServicios = new List<ServicioCita>();

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_citas_pendientes";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_idUsuario);
                    comando.Parameters.Add(param_error);
                    comando.Parameters.Add(param_mensaje);
                    var reader = await comando.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var servicio = new ServicioCita();
                        servicio.id = int.Parse(reader["idCita"].ToString());
                        servicio.nombre = reader["nombre"].ToString();
                        servicio.precio = float.Parse(reader["precio"].ToString());
                        servicio.tiempo = int.Parse(reader["tiempoServicio"].ToString());

                        servicio.tipoServicio = reader["tipo"].ToString();
                        servicio.status = reader["status"].ToString();
                        servicio.fechaInicio = reader["fechaEntrada"].ToString();
                        servicio.fechaTermino= reader["fechaSalida"].ToString();
                        listaServicios.Add(servicio);
                    }
                }
            }
            catch
            {
                return new ResponseBase<IEnumerable<ServicioCita>> { TieneError = true, Mensaje = "Error interno. Consulte al administrador del sistema.", Modelo = null };

            }
            finally
            {
                _connection.Close();
            }
            return new ResponseBase<IEnumerable<ServicioCita>> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = listaServicios };
        }
    }
}
