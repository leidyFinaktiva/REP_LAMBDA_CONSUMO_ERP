using AWSLambdaConsumoERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AWSLambdaConsumoERP.Persistence
{
    public class ConfiguracionLambdaDBContext : DbContext
    {
        public ConfiguracionLambdaDBContext(DbContextOptions<ConfiguracionLambdaDBContext> options) : base(options) 
        { 

        }

        //Entidades
        public DbSet<TrazaConsumoERP> TrazaConsumoERP { get; set; }
    }
}

