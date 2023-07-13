using AWSLambdaConsumoERP.Persistence.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AWSLambdaConsumoERP.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly IDbContextFactory<ConfiguracionLambdaDBContext> _lambdaContext;
        private readonly ConfiguracionLambdaDBContext _dbContext;

        public BaseRepository(IDbContextFactory<ConfiguracionLambdaDBContext> lambdaContext)
        {
            _lambdaContext = lambdaContext;
            var context = _lambdaContext.CreateDbContext();
            _dbContext = context;
        }
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            T? t = await _dbContext.Set<T>().FindAsync(id);
            return t;
        }

        public virtual async Task<T?> GetByIdAsync(Int32 id)
        {
            T? t = await _dbContext.Set<T>().FindAsync(id);
            return t;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async virtual Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size)
        {
            return await _dbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int pageSize)
        {
            return await _dbContext.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }
    }
}

