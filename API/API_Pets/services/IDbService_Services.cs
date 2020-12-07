using API.models;
using DemoApiUsers.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApiUsers.services
{
    public interface IDbService_Services
    {
        Task<ResponseBase<IEnumerable<Servicio>>> ObtenerPorTipo(string tipo);
        Task<ResponseBase<IEnumerable<ServicioCita>>> ObtenerHistorial(int idUsuario);
        Task<ResponseBase<IEnumerable<ServicioCita>>> ObtenerCitasPendientes(int idUsuario);
        Task<ResponseBase<int>> CancelarCita(int idCita);
        Task<ResponseBase<int>> completeAppointment(int idCita);
        Task<ResponseBase<int>> AgregarCita(Cita cita);
        Task<ResponseBase<Horas<IEnumerable<string>>>> ObtenerHoras(ServicioHoras servicio);
        Task<ResponseBase<IEnumerable<Venta>>> getSales();
        Task<ResponseBase<IEnumerable<Venta>>> getSalesPerDate(string startDate, string endDate);
        Task<ResponseBase<IEnumerable<DetailedService>>> getServices();
        Task<ResponseBase<int>> updateService(DetailedService service);
        Task<ResponseBase<int>> addService(DetailedService service);
        Task<ResponseBase<int>> deleteService(int id);
        Task<ResponseBase<ClinicHours>> getClinicHours();
        Task<ResponseBase<IEnumerable<Capacity>>> getCapacity();
        Task<ResponseBase<int>> updateClinicHours(ClinicHours hours);
        Task<ResponseBase<int>> updateCapacity(Capacity capacity);
        Task<ResponseBase<IEnumerable<FullApointment>>> getAppointments();
        Task<ResponseBase<int>> addApointment(FullApointment cita);
    }
}
