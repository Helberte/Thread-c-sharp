using Threads.Models;
using RestSharp;
using System;

namespace Threads.API
{
    public class APIManager
    {
        public const string BASE_URL = "https://api.invertexto.com/v1/fipe/";

        public static RestResponse SendRequest(string rota, Method method, BaseRequest baseRequest)
        {
            RestClient client   = new RestClient(BASE_URL + rota);
            RestRequest request = new RestRequest() { Method = method };

            request.AddObject(baseRequest);

            // envia a requisição
            RestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
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
