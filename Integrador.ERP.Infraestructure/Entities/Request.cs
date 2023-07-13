using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Infraestructure.Entities
{
    public class RequestIntegrador
    {
        public string Url { get; set; }
        public string Data { get; set; }
        public string Method { get; set; }
        public int Endpoint { get; set; }
        public RestSharp.Method MethodRest { get; set; }
        public string NombreApiConsumida { get; set; }
    }
}
