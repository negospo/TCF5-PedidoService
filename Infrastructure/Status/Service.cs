﻿using Application.DTOs.External.StatusService;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Text.Json;

namespace Infrastructure.Status
{
    public class Service : Application.Interfaces.UseCases.IPedidoStatusUseCase
    {
        public Application.DTOs.External.StatusService.PedidoStatusResponse Get(int pedidoId)
        {
            var client = new RestClient(Settings.APIStatusPath);
            var request = new RestRequest($"Pedido/{pedidoId}", Method.Get);
            var response = client.Execute<Application.DTOs.External.StatusService.PedidoStatusResponse>(request);
            if (!response.IsSuccessful)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }

        public IEnumerable<Application.DTOs.External.StatusService.PedidoStatusResponse> List(IEnumerable<int> pedidoIds)
        {
            var client = new RestClient(Settings.APIStatusPath);
            var request = new RestRequest("Pedido/list", Method.Post);
            request.AddJsonBody(pedidoIds);

            var response = client.Execute<List<Application.DTOs.External.StatusService.PedidoStatusResponse>>(request);
         
            if (!response.IsSuccessful)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }

        public IEnumerable<PedidoStatusResponse> ListByStatus(Application.Enums.PedidoStatus status)
        {
            var client = new RestClient(Settings.APIStatusPath);
            var request = new RestRequest($"Pedido/listByStatus?status={(int)status}", Method.Post);
            var response = client.Execute<List<Application.DTOs.External.StatusService.PedidoStatusResponse>>(request);

            if (!response.IsSuccessful)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }

        public bool Save(Application.DTOs.External.StatusService.PedidoStatusRequest pedido)
        {
            var client = new RestClient(Settings.APIStatusPath);
            var request = new RestRequest("Pedido/save", Method.Post);
            request.AddJsonBody(pedido);
            var response = client.Execute<bool>(request);
            if (!response.IsSuccessful)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }
    }
}
