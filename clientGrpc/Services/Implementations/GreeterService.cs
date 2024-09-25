using greetProto;

namespace clientGrpc.Services.Implementations
{
    public class GreeterService : IGreeterService
    {
        private readonly GreeterGrpcService.GreeterGrpcServiceClient _greeterClient;
        public GreeterService(GreeterGrpcService.GreeterGrpcServiceClient greeterClient) 
        {
            _greeterClient = greeterClient;
        }
        public async Task<string> SayHello(string message)
        {
            var request = new HelloRequest { Name = message };

            var response = await _greeterClient.SayHelloAsync(request);

            return response.Message;
        }       
    }
}
