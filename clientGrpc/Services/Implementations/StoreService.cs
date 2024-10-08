﻿using storeProto;
using System.Reflection.Metadata.Ecma335;

namespace clientGrpc.Services.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly StoreGrpcService.StoreGrpcServiceClient _client;

        public StoreService(StoreGrpcService.StoreGrpcServiceClient client)
        {
            _client = client;
        }

        public async Task<StoreGrpc> GetStoreByCode(string code)
        {
            var request = new RequestCode { Code = code };
            var response = await _client.getStoreGrpcAsync(request);
            return response;
        }

        public async Task<List<StoreGrpc>> GetStoresByState(bool active)
        {
            var request = new StoreStateRequest { Active = active };
            var response = await _client.GetStoresByStateAsync(request);

            return response.Stores.ToList();
        }

        public async Task<StoreGrpc> CreateStore(StoreGrpc store) {

            var response = await _client.createStoreAsync(store);
            return response;

        }

        public async Task<StoreGrpc> ChangeStoreState(string code, bool active) { 
            var request = new ChangeStoreStateRequest { Code = code, Active = active };

            var response = await _client.changeStoreStateAsync(request);

            return response;
        }

        
        public async Task<StockGrpc> AssignProductToStore(string storeCode, string productCode)
        {
            var request = new AssignProductRequest { StoreCode = storeCode, ProductCode = productCode };
            var response = await _client.assignProductToStoreAsync(request);
            return response; 
        }

        public async Task<StoreGrpc> AssignUserToStore(string storeCode, int userId)
        {
            var request = new AssignUserRequest { StoreCode = storeCode, UserId = userId };
            var response = await _client.assignUserToStoreAsync(request);
            return response;
        }

        public async Task<RemoveProductResponse> RemoveProductFromStore(string storeCode, string productCode)
        {
            var request = new RemoveProductRequest { StoreCode = storeCode, ProductCode = productCode };
            var response = await _client.removeProductFromStoreAsync(request);
            return response;
        }

        public async Task<RemoveUserResponse> RemoveUserFromStore(string storeCode, int userId)
        {
            var request = new RemoveUserRequest { StoreCode = storeCode, UserId = userId };
            var response = await _client.removeUserFromStoreAsync(request);
            return response;
        }

        
    }
}
