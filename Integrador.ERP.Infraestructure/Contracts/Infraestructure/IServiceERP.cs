using com.sun.tools.@internal.ws.processor.model;
using Integrador.ERP.Application.Contracts.Persistence;
using Integrador.ERP.Application.Models;
using Integrador.ERP.Domain.Entities;
using Integrador.ERP.Infraestructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Application.Contracts.Infraestructure
{
    public interface IServiceERP<T>: IApiService<T> where T : class
    {
        T ConsumirServicioERP(RequestIntegrador _request);
        Task AddResponseApi(ResponseVm response, string data, int FK_endpoint);
    }
}
