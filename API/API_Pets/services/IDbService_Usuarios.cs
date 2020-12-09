using API.models;
using API_Pets.models;
using DemoApiUsers.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pets.services
{
    public interface IDbService_Usuarios
    {
        Task<ResponseBase<int>> logIn(Usuario usuario);
        Task<ResponseBase<int>> registro(Usuario usuario);
        Task<ResponseBase<int>> registerEmployee(Usuario usuario);
        Task<ResponseBase<Usuario>> getUser(int id);
        Task<ResponseBase<IEnumerable<Usuario>>> getUsers(int id);
        Task<ResponseBase<int>> updateUser(Usuario user);
        Task<ResponseBase<int>> deleteUser(int id);
    }
}
