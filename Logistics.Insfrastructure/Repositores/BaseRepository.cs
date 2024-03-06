using Logistics.Domain.Interfaces.Repositories;
using Logistics.Insfrastructure.Data;

namespace Logistics.Insfrastructure.Repositores
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly LogisticsContext _context;
        public BaseRepository(LogisticsContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(TEntity delete)
        {
            _context.Remove(delete);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(TEntity insert)
        {
            await _context.AddAsync(insert);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity update)
        {
            _context.Update(update);
            await _context.SaveChangesAsync();
        }
    }
}
