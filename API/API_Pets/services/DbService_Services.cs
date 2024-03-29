﻿using API.models;
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

        public async Task<ResponseBase<IEnumerable<FullApointment>>> getAppointments(int idRol)
        {
            try
            {
                var appointments = new List<FullApointment>();
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    var param_idRol = new SqlParameter("@idRol", System.Data.SqlDbType.Int);
                    param_idRol.Value = idRol;

                    string sql = "mt_get_appointments";
                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(param_idRol);

                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Console.WriteLine(reader);
                        var appointment = new FullApointment();
                        appointment.id = int.Parse(reader["id"].ToString());
                        appointment.username = reader["username"].ToString();
                        appointment.service = reader["service"].ToString();
                        appointment.enterDate = reader["enterDate"].ToString();
                        appointment.outDate = reader["outDate"].ToString();
                        appointment.status = reader["status"].ToString();
                        appointments.Add(appointment);
                    }
                }

                return new ResponseBase<IEnumerable<FullApointment>> { TieneError = false, Mensaje = "Appointments obtained correctly.", Modelo = appointments };
            }
            catch
            {
                return new ResponseBase<IEnumerable<FullApointment>> { TieneError = true, Mensaje = "Error while obtaining appointments.", Modelo = null };
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<ResponseBase<int>> addService(DetailedService service)
        {
            var param_name = new SqlParameter("@service", System.Data.SqlDbType.NVarChar, 100);
            param_name.Value = service.nombre;

            var param_price = new SqlParameter("@price", System.Data.SqlDbType.Money);
            param_price.Value = service.precio;

            var param_time = new SqlParameter("@time", System.Data.SqlDbType.Int);
            param_time.Value = service.tiempo;

            var param_type = new SqlParameter("@type", System.Data.SqlDbType.NVarChar, -1);
            param_type.Value = service.type;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@message", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_add_service";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_name);
                    comando.Parameters.Add(param_price);
                    comando.Parameters.Add(param_time);
                    comando.Parameters.Add(param_type);
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
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = service.id };
        }

        public async Task<ResponseBase<int>> updateService(DetailedService service)
        {
            var param_id = new SqlParameter("@id", System.Data.SqlDbType.Int);
            param_id.Value = service.id;

            var param_name = new SqlParameter("@service", System.Data.SqlDbType.NVarChar, 100);
            param_name.Value = service.nombre;

            var param_price = new SqlParameter("@price", System.Data.SqlDbType.Money);
            param_price.Value = service.precio;

            var param_time = new SqlParameter("@time", System.Data.SqlDbType.Int);
            param_time.Value = service.tiempo;

            var param_type = new SqlParameter("@type", System.Data.SqlDbType.NVarChar, -1);
            param_type.Value = service.type;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@message", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_update_service";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_name);
                    comando.Parameters.Add(param_price);
                    comando.Parameters.Add(param_time);
                    comando.Parameters.Add(param_type);
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
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = service.id };
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
        public async Task<ResponseBase<IEnumerable<Capacity>>> getCapacity()
        {
            try
            {
                var capacityList = new List<Capacity>();
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_get_capacity";
                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var cap = new Capacity();
                        cap.id = int.Parse(reader["id"].ToString());
                        cap.numberCapacity = int.Parse(reader["capacidad"].ToString());
                        cap.description = reader["descripcion"].ToString(); capacityList.Add(cap);
                    }
                }

                return new ResponseBase<IEnumerable<Capacity>> { TieneError = false, Mensaje = "Capacity obtained correctly.", Modelo = capacityList };
            }
            catch
            {
                return new ResponseBase<IEnumerable<Capacity>> { TieneError = true, Mensaje = "Error while obtaining capacity.", Modelo = null };
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<ResponseBase<int>> updateCapacity(Capacity capacity)
        {
            var param_id = new SqlParameter("@id", System.Data.SqlDbType.Int);
            param_id.Value = capacity.id;
            var param_capacity = new SqlParameter("@capacity", System.Data.SqlDbType.Int);
            param_capacity.Value = capacity.numberCapacity;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@message", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_update_capacity";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_id);
                    comando.Parameters.Add(param_capacity);
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
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = 1 };
        }

        public async Task<ResponseBase<IEnumerable<Rol>>> getRoles()
        {
            try
            {
                var roles = new List<Rol>();
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_get_roles";
                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var rol = new Rol();
                        rol.id = int.Parse(reader["idRol"].ToString());
                        rol.description = reader["nombre"].ToString();
                        roles.Add(rol);
                    }
                }

                return new ResponseBase<IEnumerable<Rol>> { TieneError = false, Mensaje = "Roles obtained correctly.", Modelo = roles };
            }
            catch
            {
                return new ResponseBase<IEnumerable<Rol>> { TieneError = true, Mensaje = "Error while obtaining roles.", Modelo = null };
            }
            finally
            {
                _connection.Close();
            }
        }
        public async Task<ResponseBase<ClinicHours>> getClinicHours()
        {
            try
            {
                var hours = new ClinicHours();
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_get_hours";
                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        hours.openingHour = reader["horaApertura"].ToString();
                        hours.closingHour = reader["horaCierre"].ToString();
                    }
                }

                return new ResponseBase<ClinicHours> { TieneError = false, Mensaje = "Hours obtained correctly.", Modelo = hours };
            }
            catch
            {
                return new ResponseBase<ClinicHours> { TieneError = true, Mensaje = "Error while obtaining services.", Modelo = null };
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<ResponseBase<int>> updateClinicHours(ClinicHours hours)
        {
            var param_opening = new SqlParameter("@opening", System.Data.SqlDbType.NVarChar, 50);
            param_opening.Value = hours.openingHour;
            var param_closing = new SqlParameter("@closing", System.Data.SqlDbType.NVarChar, 50);
            param_closing.Value = hours.closingHour;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@message", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_update_hours";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_opening);
                    comando.Parameters.Add(param_closing);
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
            return new ResponseBase<int> { TieneError = (bool)param_error.Value, Mensaje = param_mensaje.Value.ToString(), Modelo = 1 };
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

        public async Task<ResponseBase<int>> completeAppointment(int idCita, int idEmployee)
        {
            var param_idCita = new SqlParameter("@idcita", System.Data.SqlDbType.Int);
            param_idCita.Value = idCita;
            var param_idEmployee = new SqlParameter("@idemployee", System.Data.SqlDbType.Int);
            param_idEmployee.Value = idEmployee;

            var param_error = new SqlParameter("@error", System.Data.SqlDbType.Bit);
            param_error.Direction = System.Data.ParameterDirection.Output;
            var param_mensaje = new SqlParameter("@mensaje", System.Data.SqlDbType.NVarChar, -1);
            param_mensaje.Direction = System.Data.ParameterDirection.Output;

            try
            {
                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "mt_complete_appointment";
                    SqlCommand comando = new SqlCommand(sql, _connection);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(param_idCita);
                    comando.Parameters.Add(param_idEmployee);
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

        public async Task<ResponseBase<IEnumerable<string>>> getPermits(int idRol)
        {
            var param_idusuario = new SqlParameter("@idRol", System.Data.SqlDbType.Int);
            param_idusuario.Value = idRol;

            try
            {
                _connection.Open();

                var permits = new List<string>();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = $"mt_get_permits";
                    SqlCommand command = new SqlCommand(sql, _connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(param_idusuario);

                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        string permit = reader["ventana"].ToString();
                        permits.Add(permit);
                    }
                }

                return new ResponseBase<IEnumerable<string>> { TieneError = false, Mensaje = "Permits obtained correctly", Modelo = permits };
            }
            catch
            {
                return new ResponseBase<IEnumerable<string>> { TieneError = true, Mensaje = "Error interno. Consulte al administrador del sistema.", Modelo = null };
            }
            finally
            {
                _connection.Close();
            }
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

        public async Task<ResponseBase<int>> addApointment(FullApointment cita)
        {
            var param_idUsuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
            param_idUsuario.Value = cita.id;
            var param_idServicio = new SqlParameter("@idService", System.Data.SqlDbType.Int);
            param_idServicio.Value = cita.service;
            var param_inicio = new SqlParameter("@inicio", System.Data.SqlDbType.DateTime);
            param_inicio.Value = cita.enterDate;

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
                    string sql = "mt_add_appointment";
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
