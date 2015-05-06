﻿using System.Threading.Tasks;
using NQuandl.Client.Entities;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Client
{
    public class QuandlJsonService : QuandlService
    {
        public QuandlJsonService(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(OptionalRequestParameters optional = null)
            where TEntity : QuandlEntity, new()
        {
            var request = new DeserializeEntityRequest<TEntity> {Optional = optional};
            return await GetAsync(request);
        }

        public async Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            IDeserializedEntityRequest<TEntity> request)
            where TEntity : QuandlEntity
        {
            var stringResponse = await GetStringAsync(request);
            return new DeserializedEntityResponse<TEntity>(stringResponse, request.Mapper);
        }

        public async Task<DeserializedJsonResponse<TResponse>> GetAsync<TResponse>(IQuandlJsonRequest<TResponse> request)
            where TResponse : JsonResponse
        {
            var stringResponse = await GetStringAsync(request);
            return new DeserializedJsonResponse<TResponse>(stringResponse);
        }
    }
}