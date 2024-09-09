using LachesBrag.Context;
using LachesBrag.Models;
using LanchesBrag.Models;

namespace LachesBrag.Repositories.Interfaces
{
    public class PedidoRepository : IPeddidoRepository // Define a classe 'PedidoRepository' que implementa a interface 'IPeddidoRepository'
    {
        private readonly AppDbContext _context; // Declara uma variável privada para armazenar o contexto do banco de dados
        private readonly CarrinhoCompra _carrinho; // Declara uma variável privada para armazenar o carrinho de compras

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinho) // Construtor da classe que recebe o contexto do banco de dados e o carrinho de compras como parâmetros
        {
            _context = context; // Inicializa a variável '_context' com o valor passado como parâmetro
            _carrinho = carrinho; // Inicializa a variável '_carrinho' com o valor passado como parâmetro
        }

        public void Criar_pediddo(Pedido pedido) // Método público para criar um pedido
        {
            pedido.PedidoEnviado = DateTime.Now; // Define a data e hora de envio do pedido como o momento atual
            _context.Pedidos.Add(pedido); // Adiciona o pedido ao contexto do banco de dados
            _context.SaveChanges(); // Salva as alterações no banco de dados

            var carrinho_compra_itens = _carrinho.GetCarrinhoCompraItens(); // Obtém os itens do carrinho de compras
            foreach (var item in carrinho_compra_itens) // Loop através de cada item no carrinho de compras
            {
                var pedidoDetalhe = new PedidoDetalhe // Cria uma nova instância de 'PedidoDetalhe'
                {
                    Quantidade = item.Quantidade, // Define a quantidade do item
                    LancheId = item.Lanche.LancheId, // Define o ID do lanche
                    PedidoId = pedido.PedidoId, // Define o ID do pedido
                    Preco = item.Lanche.Preco // Define o preço do lanche
                };
                _context.PedidosDetalhe.Add(pedidoDetalhe); // Adiciona o 'PedidoDetalhe' ao contexto do banco de dados
            }
            _context.SaveChanges(); // Salva as alterações no banco de dados
        }
    }

}
