using clientGrpc.Services.Implementations;
using clientGrpc.Services;
using clientProto;

var URL_FRONT = "http://localhost:3000";
var URL_SERVER = "https://localhost:9091";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configura los CORS para que funcione en el front
builder.Services.AddCors(options =>
{
    options.AddPolicy("CQRS",
        builder => builder
            .WithOrigins(URL_FRONT)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var httpHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, sslPolicyErrors) =>
    {       
        return cert?.Issuer == cert?.Subject;
    }
};

// Agrega el cliente gRPC
builder.Services.AddGrpcClient<Greeter.GreeterClient>(options =>
{
    options.Address = new Uri(URL_SERVER); // URL del servidor
})
.ConfigurePrimaryHttpMessageHandler(() => httpHandler);

// Se debe agregar al scope el servicio para consumir en el controlador
builder.Services.AddScoped<IGreeterService, GreeterService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Siempre debe ir antes de que pase por el middleware de Authorization
app.UseCors("CQRS");

app.UseAuthorization();

app.MapControllers();

app.Run();
