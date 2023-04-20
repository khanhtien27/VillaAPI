using VillaAPI.Data;
using VillaAPI.Models;
using VillaAPI.VillaRespository.IVillaRespository;

namespace VillaAPI.VillaRespository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _context;
        public VillaNumberRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(VillaNumber entity)
        {
           _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
