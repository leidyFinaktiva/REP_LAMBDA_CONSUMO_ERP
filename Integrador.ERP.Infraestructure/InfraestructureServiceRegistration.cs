using Integrador.ERP.Application.Contracts.Infraestructure;
using Integrador.ERP.Application.Contracts.Persistence;
using Integrador.ERP.Application.Models;
using Integrador.ERP.Infraestructure.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Infraestructure
{
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection AddInfraestructuraServicesIntegrator(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));
            services.AddScoped(typeof(IApiService<>), typeof(ApiService<>));
            services.AddScoped(typeof(IServiceERP<>), typeof(ServiceERP<>));
            return services;
        }
    }
}
