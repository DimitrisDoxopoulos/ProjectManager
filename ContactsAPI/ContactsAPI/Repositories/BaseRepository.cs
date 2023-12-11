using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Project = ContactsAPI.Models.Project;

namespace ContactsAPI.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly ContactsAppContext _context;
        private readonly DbSet<T> _table;

        public BaseRepository(ContactsAppContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public virtual async void AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public Project AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? existing = await _table.FindAsync(id);
            if (existing is not null)
            {
                _table.Remove(existing);
                return true;
            }

            return false;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _table.ToListAsync();
            return entities;
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            var entity = await _table.FindAsync(id);
            return entity;
        }

        public async Task<int> GetCountAsync()
        {
            var count = await _table.CountAsync();
            return count;
        }

        public virtual async void UpdateAsync(T entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
