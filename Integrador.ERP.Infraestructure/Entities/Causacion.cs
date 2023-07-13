﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Domain.Entities
{
    public class Causacion
    {
        public string subsidiary { get; set; } = string.Empty;
        public string creditNumber { get; set; } = string.Empty;
        public string currency { get; set; } = "1";
        public string exchangeRate { get; set; } = string.Empty;
        public string transactionDate { get; set; } = string.Empty;//YYYY/MM/DD
        public string memo { get; set; } = string.Empty;
        public string taxTransactionType { get; set; } = string.Empty;

        public List<ItemAccrual> items { get; set; } = new List<ItemAccrual>();
    }

    public class ItemAccrual
    {
        public int account { get; set; }
        public string name { get; set; } = string.Empty;
        public string memo { get; set; } = string.Empty;
        public string credit { get; set; } = string.Empty;
        public string debit { get; set; } = string.Empty;
        public string totalAmount { get; set; } = string.Empty;
    }
}
