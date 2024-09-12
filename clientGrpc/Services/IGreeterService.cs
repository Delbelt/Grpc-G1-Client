namespace clientGrpc.Services
{
    public interface IGreeterService
    {
        // REFERENCIA: rpc SayHello(HelloRequest) returns(HelloReply) - Como esta representado en el archivo .proto
        // TRADUCCION: SayHello(string) returns string - Esta es la forma que debe adoptar el metodo
        Task<string> SayHello(string message);        
    }
}
