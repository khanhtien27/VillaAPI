using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaAPI.Data;
using VillaAPI.Models;
using VillaAPI.VillaRespository.IVillaRespository;

namespace VillaAPI.VillaRespository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> Dbset;  
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            //_context.VillaNumbers.Include(u => u.villa).ToList();
            this.Dbset = _context.Set<T>();
        }
        public async Task Create(T entity)
        {
            await Dbset.AddAsync(entity);
            await Save();
        }

        public async Task<T> Get(Expression<Func<T, bool>> fillter = null, bool track = true, string? includeProperties = null)
        {
            IQueryable<T> query = Dbset;
            if (!track)
            {
                query = query.AsNoTracking();
            }
            if (fillter != null)
            {
                query = query.Where(fillter);
            }
            if(includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filler = null, string? includeProperties = null)
        {
            IQueryable<T> query = Dbset;
            if (filler != null)
            {
                query = query.Where(filler);
            }
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            Dbset.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
