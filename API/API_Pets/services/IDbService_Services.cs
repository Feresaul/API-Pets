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
        Task<ResponseBase<int>> AgregarCita(Cita cita);
        Task<ResponseBase<Horas<IEnumerable<string>>>> ObtenerHoras(ServicioHoras servicio);
    }
}
