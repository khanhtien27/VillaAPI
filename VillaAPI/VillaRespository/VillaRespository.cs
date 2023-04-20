
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaAPI.Data;
using VillaAPI.Models;

namespace VillaAPI.VillaRespository.IVillaRespository
{
    public class VillaRespository :  Repository<Villa>, IVillaRespository
    {
        private readonly ApplicationDbContext _context;
        public VillaRespository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(Villa entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
