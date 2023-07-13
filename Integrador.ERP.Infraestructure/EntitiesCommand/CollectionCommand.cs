﻿using Integrador.ERP.Application.Models;
using MediatR;

namespace Integrador.ERP.Application.Features.JournalEntry.Commands.Collections
{
    public class CollectionCommand : IRequest<ResponseVm>
    {
        public string subsidiary { get; set; } = string.Empty;
        public string creditNumber { get; set; } = string.Empty;
        public string currency { get; set; } = "1";
        public string exchangeRate { get; set; } = string.Empty;
        public string transactionDate { get; set; } = string.Empty;//YYYY/MM/DD
        public string memo { get; set; } = string.Empty;
        public string taxTransactionType { get; set; } = string.Empty;

        public List<ItemCollectionVM> items { get; set; } = new List<ItemCollectionVM>();
    }

    public class ItemCollectionVM
    {
        public int account { get; set; }
        public string name { get; set; } = string.Empty;
        public string memo { get; set; } = string.Empty;
        public string credit { get; set; } = string.Empty;
        public string debit { get; set; } = string.Empty;
        public string totalAmount { get; set; } = string.Empty;
    }
}
