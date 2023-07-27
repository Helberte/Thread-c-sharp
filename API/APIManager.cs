using Threads.Models;
using RestSharp;
using System;
using Newtonsoft.Json;
using static Threads.MainActivity;
using System.Collections.Generic;

namespace Threads.API
{
    public class APIManager
    {
        public const string BASE_URL = "https://api.invertexto.com/v1/fipe/";

        public static RestResponse SendRequest(string rota, Method method, BaseRequest baseRequest)
        {        
            RestRequest request = new RestRequest() { Method = Method.Get };
            request.AddObject(baseRequest);

            // envia a requisição
            RestResponse response = GetRestClientSingleton.GetRestClient(rota).Execute(request);

            if (response.IsSuccessful)
            {
                if (response.Content != null)
                    return response;
                else
                    throw new Exception("Servidor retornou uma resposta vazia.");
            }
            else
                throw new Exception(response.StatusCode + " - " + response.StatusDescription + " - " + response.ErrorMessage);
        }
    }
}
