using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Domain.Entities
{
    /// <summary>
    /// Representa el objeto cliente a enviar al ERP (Formato JSON)
    /// </summary>
    public class Cliente
    {
        public int id { get; set; }
        public string entityid { get; set; } = string.Empty;
        public string subsidiary { get; set; } = string.Empty;
        public string type { get; set; } = "COMPANY";
        public string idDocumentType { get; set; } = string.Empty;
        public string documentNumber { get; set; } = string.Empty;
        public string companyName { get; set; } = string.Empty;
        public string firstname { get; set; } = string.Empty;
        public string secondname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
        public string slastname { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string emailAddressNotif { get; set; } = string.Empty;
        public string emailElectronicInvoice { get; set; } = string.Empty;
        public string entitystatus { get; set; } = string.Empty;
        public string sletaxRegime { get; set; } = string.Empty;
        public string taxRegime { get; set; } = string.Empty;
        public string taxResponsibilities { get; set; } = string.Empty;
        public string taxSchema { get; set; } = string.Empty;
        public List<Direccion> addresslist { get; set; } = new List<Direccion>();
        public string receivablesAccount { get; set; } = string.Empty;
        public string currency { get; set; } = "1";
        public string terms { get; set; } = "1";
    }


    public class Direccion
    {
        public string country { get; set; } = string.Empty;
        public string addressee { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string zip { get; set; } = string.Empty;
        public string addressText { get; set; } = string.Empty;
        public string type { get; set; } = "BILLING";
        public string name { get; set; } = string.Empty;
    }
}
