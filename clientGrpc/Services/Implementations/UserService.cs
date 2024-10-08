﻿using userProto;

namespace clientGrpc.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserGrpcService.UserGrpcServiceClient _UserGrpcService;
        public UserService(UserGrpcService.UserGrpcServiceClient UserGrpcService)
        {
            _UserGrpcService = UserGrpcService;
        }

        public async Task<UserGrpc> GetUserGrpc(int id)
        {
            var fetchById = new RequestId { Id = id };

            var response = await _UserGrpcService.GetUserGrpcAsync(fetchById);

            return response;
        }

        public async Task<UserList> GetUserList()
        {
            var emptyRequest = new Empty();

            var response = await _UserGrpcService.GetAllUserGrpcAsync(emptyRequest);

            return response;
        }
    }
}
