using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Domain.Entities
{
    public class ResponseApi
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? detail { get; set; }
        public string? RequestJson { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? IdResponse { get; set; }
        public int? FK_Endpoint { get; set; }
    }
}
