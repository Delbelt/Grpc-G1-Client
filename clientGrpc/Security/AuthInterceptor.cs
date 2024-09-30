using Grpc.Core;
using Grpc.Core.Interceptors;

namespace clientGrpc.Security
{
    public class AuthInterceptor : Interceptor
    {
        private string _token;

        public AuthInterceptor()
        {
            _token = string.Empty;
        }

        public string getToken()
        {
            return _token;
        }

        public void SetToken(string token)
        {
            _token = token;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var headers = new Metadata();

            if (!string.IsNullOrEmpty(_token))
            {
                headers.Add("Authorization", $"Bearer {_token}");
            }

            context = new ClientInterceptorContext<TRequest, TResponse>(
                context.Method,
                context.Host,
                context.Options.WithHeaders(headers)
            );

            return continuation(request, context);
        }
    }
}
