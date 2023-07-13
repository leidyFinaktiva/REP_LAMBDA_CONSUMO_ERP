using AWSLambdaConsumoERP.Persistence;
using AWSLambdaConsumoERP.Persistence.Contracts.Persistence;
using AWSLambdaConsumoERP.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreLambda.DI
{
    public class DependencyResolver
    {
        public IServiceProvider ServiceProvider { get; }
        public string CurrentDirectory { get; set; }
        public Action<IServiceCollection> RegisterServices { get; }

        public DependencyResolver(Action<IServiceCollection> registerServices = null)
        {
            // Set up Dependency Injection
            var serviceCollection = new ServiceCollection();
            RegisterServices = registerServices;
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            string configurationBD = "User ID=postgres;Password=Finaktiva2023;Host=db-configurationlambda-erp.cqcuh8l6dduz.us-east-1.rds.amazonaws.com;Port=5432;Database=configuracionlambda;";
            services.AddDbContextFactory<ConfiguracionLambdaDBContext>(options =>
            options.UseNpgsql(configurationBD), ServiceLifetime.Transient);
            services.AddTransient<ConfiguracionLambdaDBContext>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            // Register other services
            RegisterServices?.Invoke(services);
        }
    }
}