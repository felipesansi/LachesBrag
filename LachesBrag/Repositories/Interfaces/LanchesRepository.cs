using LachesBrag.Context;
using LachesBrag.Models;
using Microsoft.EntityFrameworkCore;

namespace LachesBrag.Repositories.Interfaces
{
    public class LanchesRepository : ILanchesRepository
    {
        private readonly AppDbContext _context;
        public LanchesRepository(AppDbContext contexto)
        {
                _context = contexto;
        }
        public IEnumerable<Lanches> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanches> LanchesPreferidos => _context.Lanches.Where(l => l.LanchePreferido).Include(c => c.Categoria);

        public Lanches GetLanchesbyId(int lancheId)
        {
         return _context.Lanches.FirstOrDefault(l =>l.LancheId == lancheId);
        }
    }
}
