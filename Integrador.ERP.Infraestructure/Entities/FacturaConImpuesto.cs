﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Domain.Entities
{
    public class FacturaConImpuesto
    {
        public string customer { get; set; }
        public string transactionDate { get; set; }
        public string dueDate { get; set; }
        public string memo { get; set; }
        public string creditNumber { get; set; }
        public int account { get; set; }
        public string salesEffectiveDate { get; set; }
        public string subsidiary { get; set; }
        public string location { get; set; }

        public List<ItemTaxes> items { get; set; }
    }

    public class ItemTaxes 
    { 
        public string item { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public int rate { get; set; }
        public int amount { get; set; }
        public List<Taxes> taxes { get; set; }
    }

    public class Taxes
    { 
        public string taxtype { get; set; }
        public string taxcode { get; set; }
        public string taxbasis { get; set; }
        public string taxrate { get; set; }
        public string taxamount { get; set; }
    }
}
