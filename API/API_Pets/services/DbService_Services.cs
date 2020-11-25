using API.models;
using DemoApiUsers.models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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

        public async Task<ResponseBase<int>> deleteService(int id)
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
                    string sql = "mt_delete_service";
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
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = "Service deleted successfully", Modelo = Convert.ToInt32(!(bool)param_error.Value) };

        }

        public async Task<ResponseBase<IEnumerable<DetailedService>>> getServices()
        {
            try
            {
                var servicesList = new List<DetailedService>();
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_get_services";
                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var service = new DetailedService();
                        service.id = int.Parse(reader["idService"].ToString());
                        service.nombre = reader["Service"].ToString();
                        service.precio = float.Parse(reader["Price"].ToString());
                        service.tiempo = int.Parse(reader["Time"].ToString());
                        service.type = reader["Type"].ToString();
                        servicesList.Add(service);
                    }
                }

                return new ResponseBase<IEnumerable<DetailedService>> { TieneError = false, Mensaje = "Services obtained correctly.", Modelo = servicesList };
            }
            catch
            {
                return new ResponseBase<IEnumerable<DetailedService>> { TieneError = true, Mensaje = "Error while obtaining services.", Modelo = null };
            }
            finally
            {
                _connection.Close();
            }

        }

        public async Task<ResponseBase<IEnumerable<Venta>>> getSales()
        {
            try
            {
                var salesList = new List<Venta>();
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_get_sales";
                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var sale = new Venta();
                        sale.service = reader["Service"].ToString();
                        sale.employee = reader["Employee"].ToString();
                        sale.client = reader["Client"].ToString();
                        sale.date = reader["Date"].ToString();
                        sale.total = reader["Total"].ToString();
                        salesList.Add(sale);
                    }
                }

                return new ResponseBase<IEnumerable<Venta>> { TieneError = false, Mensaje = "Sales obtained correctly.", Modelo = salesList };
            }
            catch
            {
                return new ResponseBase<IEnumerable<Venta>> { TieneError = true, Mensaje = "Error while obtaining sales.", Modelo = null };
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<ResponseBase<IEnumerable<Venta>>> getSalesPerDate(string startDate, string endDate)
        {
            try
            {
                var salesList = new List<Venta>();
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_get_sales_per_date";
                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var param_start = new SqlParameter("@start_date", System.Data.SqlDbType.NVarChar, -1);
                    param_start.Value = startDate;
                    var param_end = new SqlParameter("@end_date", System.Data.SqlDbType.NVarChar, -1);
                    param_end.Value = endDate;
                    command.Parameters.Add(param_start);
                    command.Parameters.Add(param_end);

                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var sale = new Venta();
                        sale.service = reader["Service"].ToString();
                        sale.employee = reader["Employee"].ToString();
                        sale.client = reader["Client"].ToString();
                        sale.date = reader["Date"].ToString();
                        sale.total = reader["Total"].ToString();
                        salesList.Add(sale);
                    }
                }

                return new ResponseBase<IEnumerable<Venta>> { TieneError = false, Mensaje = "Sales obtained correctly.", Modelo = salesList };
            }
            catch
            {
                return new ResponseBase<IEnumerable<Venta>> { TieneError = true, Mensaje = "Error while obtaining sales.", Modelo = null };
            }
            finally
            {
                _connection.Close();
            }

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

        public async Task<ResponseBase<int>> AgregarCita(Cita cita)
        {
            var param_idUsuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
            param_idUsuario.Value = cita.idUsuario;
            var param_idServicio = new SqlParameter("@idServicio", System.Data.SqlDbType.Int);
            param_idServicio.Value = cita.idServicio;
            var param_inicio = new SqlParameter("@inicio", System.Data.SqlDbType.DateTime);
            param_inicio.Value = cita.fechaInicio;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@mensaje", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;
            var param_idCita = new SqlParameter("@idCitaOut", System.Data.SqlDbType.Int);
            param_idCita.Direction = System.Data.ParameterDirection.Output;

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_agregar_cita";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_idUsuario);
                    comando.Parameters.Add(param_idServicio);
                    comando.Parameters.Add(param_inicio);
                    comando.Parameters.Add(param_error);
                    comando.Parameters.Add(param_mensaje);
                    comando.Parameters.Add(param_idCita);
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
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = (int)param_idCita.Value };
        }

        public async Task<ResponseBase<Horas<IEnumerable<string>>>> ObtenerHoras(ServicioHoras servicio)
        {
            var param_id = new SqlParameter("@idServicio", System.Data.SqlDbType.Int);
            param_id.Value = servicio.idTipoServicio;
            var param_fecha = new SqlParameter("@fecha", System.Data.SqlDbType.DateTime);
            param_fecha.Value = servicio.fechaInicio;
           
            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@mensaje", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            var param_inicio = new SqlParameter("@inicio", System.Data.SqlDbType.NVarChar, 50);
            param_inicio.Direction = System.Data.ParameterDirection.Output;
            var param_termino = new SqlParameter("@termino", System.Data.SqlDbType.NVarChar, 50);
            param_termino.Direction = System.Data.ParameterDirection.Output;
            var param_tiempo = new SqlParameter("@tiempo", System.Data.SqlDbType.Int);
            param_tiempo.Direction = System.Data.ParameterDirection.Output;

            var listaHoras = new List<string>();
            var modelo = new Horas<IEnumerable<string>>();

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_obtener_horas";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_id);
                    comando.Parameters.Add(param_fecha);
                    comando.Parameters.Add(param_error);
                    comando.Parameters.Add(param_mensaje);
                    comando.Parameters.Add(param_inicio);
                    comando.Parameters.Add(param_termino);
                    comando.Parameters.Add(param_tiempo);
                    var reader = await comando.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        DateTime addedDate = (DateTime)reader["fechaEntrada"];
                        string addedDateText = addedDate.ToString("yyyy/mm/dd HH:mm:ss", CultureInfo.InvariantCulture);
                        listaHoras.Add(addedDateText);
                    }
                    modelo.arrayHoras = listaHoras;
                }
            }
            catch
            {
                return new ResponseBase<Horas<IEnumerable<string>>> { TieneError = true, Mensaje = "Error interno. Consulte al administrador del sistema.", Modelo = null };

            }
            finally
            {
                _connection.Close();
            }
            
            modelo.inicio = param_inicio.Value.ToString();
            modelo.termino = param_termino.Value.ToString();
            modelo.tiempo = (int)param_tiempo.Value;
            return new ResponseBase<Horas<IEnumerable<string>>> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = modelo };

        }
    }
}
