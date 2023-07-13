using AutoMapper;
using Integrador.ERP.Application.Features.Clientes.Commands.CreateClienteERP;
using Integrador.ERP.Application.Features.Invoices.Commands.Invoices;
using Integrador.ERP.Application.Features.JournalEntry.Commands.Accruals;
using Integrador.ERP.Application.Features.JournalEntry.Commands.Collections;
using Integrador.ERP.Application.Features.JournalEntry.Commands.Credits;
using Integrador.ERP.Application.Features.JournalEntry.Commands.Disbursements;
using Integrador.ERP.Application.Features.JournalEntry.Commands.Interests;
using Integrador.ERP.Application.Features.Payments.Commands.Payments;
using Integrador.ERP.Application.Models;
using Integrador.ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Injection Dependecy
            //////Cliente
            //Query
            //CreateMap<Funder, FunderListVm>().ReverseMap();
            //CreateMap<Funder, FunderDetailVm>().ReverseMap();
            ////COMMANDS
            CreateMap<Cliente, CreateClientCommand>().ReverseMap();
            CreateMap<ComprobanteDiario, DisbursementCommand>().ReverseMap();
            CreateMap<Interes, InterestCommand>().ReverseMap();
            CreateMap<Recaudo, CollectionCommand>().ReverseMap();
            CreateMap<Credito, CreditCommand>().ReverseMap();
            CreateMap<Causacion, AccrualCommand>().ReverseMap();
            CreateMap<Pago, CustomerPaymentCommand>().ReverseMap();
            CreateMap<FacturaConImpuesto, InvoiceTaxCommand>().ReverseMap();
            CreateMap<FacturaSinImpuesto, InvoiceCommand>().ReverseMap();
            CreateMap<ItemTaxes, ItemTaxesVM>().ReverseMap();
            CreateMap<Taxes, TaxesVM>().ReverseMap();
            CreateMap<ItemInvoice, ItemInvoiceVM>().ReverseMap();
            CreateMap<Item, ItemVM>().ReverseMap();
            CreateMap<ItemInterest, ItemInterestVM>().ReverseMap();
            CreateMap<ItemCollection, ItemCollectionVM>().ReverseMap();
            CreateMap<ItemCredit, ItemCreditVM>().ReverseMap();
            CreateMap<ItemAccrual, ItemAccrualVM>().ReverseMap();
            CreateMap<Direccion, DireccionVM>().ReverseMap();
            CreateMap<ResponseERP, ResponseVm>().ReverseMap();
            CreateMap<dataERP, data>().ReverseMap();
        }
    }
}
