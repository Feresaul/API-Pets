using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoApiUsers.models;

namespace DemoApiUsers.services
{
    public interface IDbService
    {
        Task<ResponseBase<IEnumerable<Usuario>>> ObtenerUsuario();
        Task<ResponseBase<Usuario>> ObtenerUsuarioPorId(int id);
        Task<ResponseBase<Usuario>> GuardarUsuario(Usuario modelo);
        Task<ResponseBase<bool>> EliminarUsuario(int id);
    }
}
