using clientProto;

namespace clientGrpc.Services.Implementations
{
    public class GreeterService : IGreeterService
    {
        private readonly Greeter.GreeterClient _greeterClient;
        public GreeterService(Greeter.GreeterClient greeterClient) 
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
