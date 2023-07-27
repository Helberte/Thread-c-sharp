using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Threads.API
{
    public class GetRestClientSingleton
    {
        private const string BASE_URL = "https://api.invertexto.com/v1/fipe/";

        private static RestClient cliente;

        private GetRestClientSingleton() { }

        public static RestClient GetRestClient(string rota)
        {
            if (cliente == null)
            {
                var opcoes = new RestClientOptions(BASE_URL + rota)
                {
                    ThrowOnAnyError = true,
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                    MaxTimeout = 1000
                };

                cliente = new RestClient(opcoes);
            }

            return cliente;         
        }
    }
}