using LachesBrag.Context;
using LachesBrag.Models;
using Microsoft.EntityFrameworkCore;

namespace LachesBrag.Areas.Admin.Servicos
{
    public class RelatorioLanchesServices
    {
        private readonly AppDbContext context;

        public RelatorioLanchesServices(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Lanche>> MostrarLanchesRelatorio()
        {
            var lanches = await context.Lanches.ToListAsync();

            if (lanches is null)
            {
                return default(IEnumerable<Lanche>);
            }
            return lanches;
        }
        public async Task<IEnumerable<Categoria>> MostrarCategoriasRelatorio()
        {
            var categoria = await context.Categorias.ToListAsync();

            if (categoria is null)
            {
                return default(IEnumerable<Categoria>);
            }
            return categoria;
        }
    }
}