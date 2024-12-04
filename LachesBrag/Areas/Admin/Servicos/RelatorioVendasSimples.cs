using LachesBrag.Context;
using LachesBrag.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LachesBrag.Areas.Admin.Servicos
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext _context;

        public RelatorioVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Pedido>, int totalLanches, decimal valorTotalVendas)> FindByDateAsync(DateTime? dataMin, DateTime? dataMax)
        {
            var resultado = _context.Pedidos
                .Include(p => p.PedidoItens)
                .ThenInclude(pi => pi.Lanche)
                .AsQueryable();

            if (dataMin.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= dataMin.Value);
            }

            if (dataMax.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado <= dataMax.Value);
            }

            var pedidos = await resultado.ToListAsync();

            
            foreach (var pedido in pedidos)
            {
                pedido.TotalItensPedidos = pedido.PedidoItens.Sum(pi => pi.Quantidade);
                pedido.PedidoTotal = pedido.PedidoItens.Sum(pi => pi.Preco * pi.Quantidade);
            }

            var totalLanches = pedidos.SelectMany(p => p.PedidoItens).Sum(pi => pi.Quantidade);
            var valorTotalVendas = pedidos.SelectMany(p => p.PedidoItens).Sum(pi => pi.Preco * pi.Quantidade);

            return (pedidos, totalLanches, valorTotalVendas);
        }

    }
}
