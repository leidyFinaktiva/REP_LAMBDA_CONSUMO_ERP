using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Application.Models
{
    public class ResponseVm
    {
        public string? code { get; set; }
        public int? id { get; set; }
        public string? errors { get; set; }
        public string? detail { get; set; }
        public List<data>? data { get; set; }
    }
    public class data
    {
        public string? id { get; set; }
        public string? type { get; set; }
        public string? idDocumentType { get; set; }
        public string? documentNumber { get; set; }
        public string? name { get; set; }
    }
}
