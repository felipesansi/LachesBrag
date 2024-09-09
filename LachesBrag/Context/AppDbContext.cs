using LachesBrag.Models;
using Microsoft.EntityFrameworkCore;

namespace LachesBrag.Context
{
    public class AppDbContext : DbContext // colocar :  DbContext para colocar o contex para funcionar
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) // Construtor  do DbContext
        { 
        }
        // Criando Tabelas
      
        public DbSet<Categoria> Categorias { get; set; } // Categoria é os elementos dentro do Tabela <Categorias>
        public DbSet<Lanche> Lanches { get; set; } // Laches é os elementos dentro do Tabela <lanches>
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; } // CarrinhoCompraItens é os elementos dentro do Tabela <CarrinhoCompraItem>
        public DbSet<Pedido> Pedidos { get; set; } // Pedidos é os elementos dentro da Tabela <pedido>
        public DbSet<PedidoDetalhe> PedidosDetalhe { get; set; } // PedidosDetalhe é os elementos dentro da Tabela <PedidosDetalhe>
    }
}
