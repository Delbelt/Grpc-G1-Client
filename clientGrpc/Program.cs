using clientGrpc.Services.Implementations;
using clientGrpc.Services;
using clientGrpc.Security;

using authProto;
using productProto;
using greetProto;
using userProto;
using storeProto;
using stockProto;
using kafkaProto;
using orderItemProto;
using purchaseOrderProto;
using dispatchOrderProto;
using OrderProcessingProto;

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

builder.Services.AddSingleton<AuthInterceptor>();

// SEGURIDAD

builder.Services.AddGrpcClient<AuthGrpcService.AuthGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
})
.ConfigurePrimaryHttpMessageHandler(() => httpHandler);

// KAFKA

builder.Services.AddGrpcClient<KafkaGrpcService.KafkaGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
})
.ConfigurePrimaryHttpMessageHandler(() => httpHandler)
.AddInterceptor(provider => provider.GetRequiredService<AuthInterceptor>());


// Agregar los clientes Grpc
builder.Services.AddGrpcClient<GreeterGrpcService.GreeterGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
})
.ConfigurePrimaryHttpMessageHandler(() => httpHandler)
.AddInterceptor(provider => provider.GetRequiredService<AuthInterceptor>());

builder.Services.AddGrpcClient<UserGrpcService.UserGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
})
.ConfigurePrimaryHttpMessageHandler(() => httpHandler)
.AddInterceptor(provider => provider.GetRequiredService<AuthInterceptor>());

builder.Services.AddGrpcClient<StoreGrpcService.StoreGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
}
)
.ConfigurePrimaryHttpMessageHandler(options => httpHandler)
.AddInterceptor(provider => provider.GetRequiredService<AuthInterceptor>());

builder.Services.AddGrpcClient<StockGrpcService.StockGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
}
)
.ConfigurePrimaryHttpMessageHandler(options => httpHandler)
.AddInterceptor(provider => provider.GetRequiredService<AuthInterceptor>());

builder.Services.AddGrpcClient<ProductGrpcService.ProductGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
})
.ConfigurePrimaryHttpMessageHandler(() => httpHandler)
.AddInterceptor(provider => provider.GetRequiredService<AuthInterceptor>());

builder.Services.AddGrpcClient<PurchaseOrderGrpcService.PurchaseOrderGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
})
.ConfigurePrimaryHttpMessageHandler(() => httpHandler)
.AddInterceptor(provider => provider.GetRequiredService<AuthInterceptor>());

builder.Services.AddGrpcClient<OrderItemGrpcService.OrderItemGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
})
.ConfigurePrimaryHttpMessageHandler(() => httpHandler)
.AddInterceptor(provider => provider.GetRequiredService<AuthInterceptor>());

builder.Services.AddGrpcClient<DispatchOrderGrpcService.DispatchOrderGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
})
.ConfigurePrimaryHttpMessageHandler(() => httpHandler)
.AddInterceptor(provider => provider.GetRequiredService<AuthInterceptor>());

builder.Services.AddGrpcClient<OrderProcessingGrpcService.OrderProcessingGrpcServiceClient>(options =>
{
    options.Address = new Uri(URL_SERVER);
})
.ConfigurePrimaryHttpMessageHandler(() => httpHandler)
.AddInterceptor(provider => provider.GetRequiredService<AuthInterceptor>());

// Agregar al Scope los servicios
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IKafkaService, KafkaService>();
builder.Services.AddScoped<IGreeterService, GreeterService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddScoped<IDispatchOrderService, DispatchOrderService>();

builder.Services.AddScoped<IOrderProcessingService, OrderProcessingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Siempre debe ir antes de que pase por el Middleware de Authorization

app.UseCors("CQRS");

app.UseAuthorization();

app.MapControllers();

app.Run();
