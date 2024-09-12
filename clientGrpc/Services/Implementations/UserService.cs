using clientProto;

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
    }
}
