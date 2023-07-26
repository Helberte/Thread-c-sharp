using System.Collections.Generic;

namespace Threads.Models
{
    #region Listar Marcas

    public class MarcasRequest : BaseRequest
    {
        public MarcasRequest()
        {

        }
    }

    public class MarcasResponse
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public MarcasResponse()
        {
            this.Brand = "";
            this.Id    = 0;
        }
    }

    #endregion

    #region Listar Modelos

    public class ModelosRequest : BaseRequest
    {
        public ModelosRequest()
        {

        }
    }

    public class ModelosResponse
    {
        public string Fipe_code { get; set; }

        public string Model { get; set; }

        public string Years { get; set; }

        public ModelosResponse()
        {
            this.Fipe_code = string.Empty;
            this.Model     = string.Empty;
            this.Years     = string.Empty;
        }
    }

    #endregion

    #region Listar Anos e Preços de um Modelo

    public class AnosPrecosRequest : BaseRequest
    {
        public AnosPrecosRequest()
        {
             
        }
    }

    public class AnosPrecosResponse
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Reference { get; set; }

        public List<Anos> Years { get; set; }
         
        public AnosPrecosResponse()
        {
            Brand     = string.Empty;
            Model     = string.Empty;
            Reference = string.Empty;
            Years     = new List<Anos>();
        }
    }

    public class Anos
    {
        public string Year_id { get; set; }

        public string Model_year { get; set; }

        public string Fuel { get; set; }
         
        public decimal Price { get; set; }

        public Anos()
        {
            Year_id    = string.Empty;
            Model_year = string.Empty;
            Fuel       = string.Empty;
            Price      = 0m;
        }
    }

    #endregion
}
