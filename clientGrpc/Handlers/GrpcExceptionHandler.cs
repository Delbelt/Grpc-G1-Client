using Microsoft.AspNetCore.Mvc;

namespace clientGrpc.Handlers
{
    public static class GrpcExceptionHandler
    {
        public static IActionResult HandleGrpcException(Grpc.Core.RpcException ex, object responseDTO)
        {
            if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unauthenticated))
            {
                return new UnauthorizedObjectResult(responseDTO);
            }

            if (ex.StatusCode.Equals(Grpc.Core.StatusCode.PermissionDenied))
            {

                return new ObjectResult(responseDTO)
                {
                    StatusCode = 403
                };
            }

            if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unavailable))
            {           
                return new ObjectResult(responseDTO)
                {
                    StatusCode = 500
                };
            }

            return new NotFoundObjectResult(responseDTO);
        }
    }
}
