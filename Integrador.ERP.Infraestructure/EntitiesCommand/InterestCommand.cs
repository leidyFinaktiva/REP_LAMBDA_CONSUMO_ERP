using Integrador.ERP.Application.Models;
using Integrador.ERP.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Application.Features.JournalEntry.Commands.Interests
{
    public class InterestCommand : IRequest<ResponseVm>
    {
        public string subsidiary { get; set; }
        public string creditNumber { get; set; }
        public string currency { get; set; }
        public string exchangeRate { get; set; }
        public string transactionDate { get; set; }
        public string reversaldate { get; set; }
        public string memo { get; set; }
        public string taxTransactionType { get; set; }
        public string transactionType { get; set; }

        public List<ItemInterestVM> items { get; set; }
    }

    public class ItemInterestVM
    {
        public Int64 account { get; set; }
        public string name { get; set; }
        public string memo { get; set; }
        public string credit { get; set; }
        public string debit { get; set; }
        public string totalAmount { get; set; }
    }
}
