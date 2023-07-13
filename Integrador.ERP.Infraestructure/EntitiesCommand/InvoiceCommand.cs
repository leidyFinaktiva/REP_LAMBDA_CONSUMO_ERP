using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Application.Features.Invoices.Commands.Invoices
{
    public class InvoiceCommand
    {
        public string customer { get; set; } = string.Empty;
        public string transactionDate { get; set; } = string.Empty;
        public string dueDate { get; set; } = string.Empty;
        public string memo { get; set; } = string.Empty;
        public string creditNumber { get; set; } = string.Empty;
        public int account { get; set; }
        public string salesEffectiveDate { get; set; } = string.Empty;
        public string subsidiary { get; set; } = string.Empty;
        public string location { get; set; } = string.Empty;
        public List<ItemInvoiceVM> items { get; set; }
    }

    public class ItemInvoiceVM
    {
        public string item { get; set; } = string.Empty;
        public string quantity { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int rate { get; set; }
        public int amount { get; set; }
    }
}
