﻿using clientProto;

namespace clientGrpc.Services
{
    public interface IUserService
    {
        // PRUEBA DE PERSISTENCIA: Se genera el objetoGrpc
        Task<UserGrpc> GetUserGrpc(int id);
    }
}
