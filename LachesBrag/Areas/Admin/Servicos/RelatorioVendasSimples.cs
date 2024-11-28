using LachesBrag.Context;
using LachesBrag.Models;
using Microsoft.EntityFrameworkCore;

namespace LachesBrag.Areas.Admin.Servicos
{
    public class RelatorioVendasSimples
    {
        private readonly AppDbContext _context;

        public RelatorioVendasSimples(AppDbContext context)
        {
            _context = context;
        }

        public async Task <List<Pedido>> FindByAsync(DateTime? dataMax,DateTime? dataMini)
        {
            var consulta = from obj in _context.Pedidos select obj;

            if (dataMini.HasValue)
            {
                consulta = consulta.Where(x => x.PedidoEnviado >= dataMini.Value);

            }
            if (dataMax.HasValue)
            {
                consulta = consulta.Where(x => x.PedidoEnviado >= dataMax.Value);

            }
             return await consulta.Include(l=> l.PedidoItens).ThenInclude(l =>l.Lanche).OrderByDescending(x=>x.PedidoEnviado).ToListAsync();
        }
    }
}
