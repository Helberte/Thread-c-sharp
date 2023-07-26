using Threads.Models;
using RestSharp;

namespace Threads.API
{
    public static class RouteManager
    {
        public static RestResponse ListarMarcas(MarcasRequest request, string idVeiculo) => APIManager.SendRequest("brands/" + idVeiculo, Method.Get, request);
         
        public static RestResponse ListarModelos(ModelosRequest request, string idMarca) => APIManager.SendRequest("models/" + idMarca, Method.Get, request);

        public static RestResponse ListarAnosPrecos(AnosPrecosRequest request, string idModelo) => APIManager.SendRequest("years/" + idModelo, Method.Get, request);

    }
}
 