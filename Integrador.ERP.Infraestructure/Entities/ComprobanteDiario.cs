﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Domain.Entities
{
    /// <summary>
    /// Representa el objeto de comprobante diario a enviar al ERP (Formato JSON)
    /// Tener encuenta que es el mismo objeto base para Asiento diario Desembolso, Asiento diario Intereses,
    /// Asiento diario Recaudo, Asiento diario Causación , Asiento diario creación credito
    /// </summary>
    public class ComprobanteDiario
    {
        public string subsidiary { get; set; } = string.Empty;
        public string creditNumber { get; set; } = string.Empty;
        public string currency { get; set; } = "1";
        public string exchangeRate { get; set; } = string.Empty;
        public string transactionDate { get; set; } = string.Empty;//YYYY/MM/DD
        public string memo { get; set; } = string.Empty;
        public string taxTransactionType { get; set; } = string.Empty;
        public string transactionType { get; set; } = string.Empty;

        public List<Item> items { get; set; } = new List<Item>();
    }

    public class Item
    {
        public int account { get; set; }
        public string name { get; set; } = string.Empty;
        public string memo { get; set; } = string.Empty;
        public string credit { get; set; } = string.Empty;
        public string debit { get; set; } = string.Empty;
        public string totalAmount { get; set; } = string.Empty;
    }
}