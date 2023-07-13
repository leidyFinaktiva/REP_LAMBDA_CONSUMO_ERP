using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Domain.Entities
{
    /// <summary>
    /// Representa el objeto PAGO a enviar al ERP
    /// </summary>
    public class Pago
    {
        public int account { get; set; }
        public bool autoapply { get; set; }
        public int invoice { get; set; }
        public int customer { get; set; }
        public double value { get; set; }
        public string contractNumber { get; set; }
        public string memo { get; set; }
        public int accountToBill { get; set; }
 
    }
}
