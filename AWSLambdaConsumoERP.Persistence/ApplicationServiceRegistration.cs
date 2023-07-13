using AWSLambdaConsumoERP.Persistence;
using AWSLambdaConsumoERP.Persistence.Contracts.Persistence;
using AWSLambdaConsumoERP.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AWSLambda1.Persistence
{
    public class PersistenceServiceRegistration
    {
        public IServiceCollection AddPersistenceServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            string configurationBD = "User ID=postgres;Password=Finaktiva2023;Host=db-configurationlambda-erp.cqcuh8l6dduz.us-east-1.rds.amazonaws.com;Port=5432;Database=configuracionlambda;";
            services.AddDbContext<ConfiguracionLambdaDBContext>(options =>
            options.UseNpgsql(configurationBD));
            return services;
        }
    }
}

