using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoApiUsers.models;

namespace DemoApiUsers.services
{
    public interface IDbService_LogIn
    {
        Task<ResponseBase<Usuario_LogIn>> RegisterUser(Usuario_LogIn modelo);
    }
}
