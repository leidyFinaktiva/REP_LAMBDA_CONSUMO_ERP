using Integrador.ERP.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Domain.Entities
{
    public class ResponseTrazaApi
    {
        public string code { get; set; }
        public int id { get; set; }
        public string errors { get; set; }
        public string detail { get; set; }
        public string objectData { get; set; }
        public int endpoint { get; set; }
        public string nombreapiconsumida { get; set; }
        public List<data> data { get; set; }
    }
}
