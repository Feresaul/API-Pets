﻿using API_Pets.models;
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
    }
}
