using LachesBrag.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LachesBrag.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Criando Tabelas
        public DbSet<Categoria> Categorias { get; set; } // Categorias são os elementos dentro da tabela <Categorias>
        public DbSet<Lanche> Lanches { get; set; } // Lanches são os elementos dentro da tabela <Lanches>
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; } // CarrinhoCompraItens são os elementos dentro da tabela <CarrinhoCompraItem>
        public DbSet<Pedido> Pedidos { get; set; } // Pedidos são os elementos dentro da tabela <Pedidos>
        public DbSet<PedidoDetalhe> PedidosDetalhe { get; set; } // PedidosDetalhe são os elementos dentro da tabela <PedidosDetalhe>
    }
}
