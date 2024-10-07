using clientGrpc.DTOs;
using Grpc.Core;
using productProto;

namespace clientGrpc.Services.Implementations
{
    public class ProductService : IProductService
    {

        private readonly ProductGrpcService.ProductGrpcServiceClient _ProductGrpcService;
        public ProductService(ProductGrpcService.ProductGrpcServiceClient ProductGrpcService)
        {
            _ProductGrpcService = ProductGrpcService;
        }
        public async Task<ProductGrpc> GetProductGrpc(string code)
        {
            var fetchByCode = new RequestId { Code = code };

            var response = await _ProductGrpcService.GetProductByCodeAsync(fetchByCode);

            return response;
        }
        public async Task<ProductList> GetAllProductsGrpc()
        {
            var emptyRequest = new Empty(); 
            var response = await _ProductGrpcService.GetAllProductsAsync(emptyRequest);
            return response;
        }

        public async Task<string> CreateProductGrpc(ProductDTO productDTO)
        {
            var newProduct = new ProductGrpc
            {
                Code = productDTO.Code,
                Name = productDTO.Name,
                Size = productDTO.Size,
                Photo = Google.Protobuf.ByteString.CopyFrom(productDTO.Photo),
                Color = productDTO.Color,
                Active = true,
                
            };

            var response = await _ProductGrpcService.CreateProductAsync(newProduct);
            return response.Message;
        }

        public async Task<ProductList> GetProductsByFilter(string code, string name, string size, string color)
        {
            var filterRequest = new ProductFilterRequest
            {
                Code = code ?? "",  // Si alguno de los parámetros es nulo, envía una cadena vacía
                Name = name ?? "",
                Size = size ?? "",
                Color = color ?? ""
            };

            var response = await _ProductGrpcService.GetProductsByFilterAsync(filterRequest);

            return response;
        }

        public async Task<string> DeleteProductGrpc(string code)
        {
            var requestId = new RequestId { Code = code };
            var response = await _ProductGrpcService.DeleteProductAsync(requestId);
            return response.Message;
        }

        public async Task<string> UpdateProductGrpc(ProductDTO productDTO)
        {
            try
            {
                // Construir el mensaje ProductGrpc a partir del DTO
                var productRequest = new ProductGrpc
                {
                    Code = productDTO.Code ?? "",
                    Name = productDTO.Name ?? "",
                    Size = productDTO.Size ?? "",
                    Photo = null,
                    Color = productDTO.Color ?? "",
                    Active = false
                };

                // Llamar al método gRPC updateProduct
                var response = await _ProductGrpcService.UpdateProductAsync(productRequest);

                return response.Message;
            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(ex.StatusCode, ex.Status.Detail));
            }
        }

        public async Task<string> ModifyProductActiveGrpc(string code)
        {

            try
            {
                var requestId = new RequestId { Code = code };
                var productResponse = await _ProductGrpcService.GetProductByCodeAsync(requestId);
                if (productResponse == null)
                {
                    return "Product not found.";
                }

              
                var updatedActiveStatus = !productResponse.Active;  // Cambia el estado

                // Crear el objeto actualizado ProductGrpc
                var updatedProductRequest = new ProductGrpc
                {
                    Code = productResponse.Code,
                    Name = productResponse.Name,
                    Size = productResponse.Size,
                    Photo = productResponse.Photo,
                    Color = productResponse.Color,
                    Active = updatedActiveStatus 
                };

                // Llamar al método gRPC updateProduct para actualizar el producto
                var response = await _ProductGrpcService.UpdateProductAsync(updatedProductRequest);

                return response.Message;
            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(ex.StatusCode, ex.Status.Detail));
            }


        }

     }
}
