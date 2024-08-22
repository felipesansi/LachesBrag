using LachesBrag.Context;
using LachesBrag.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesBrag.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context; // Declara uma variável privada para armazenar a instância do contexto de banco de dados

        public CarrinhoCompra(AppDbContext context) // Construtor da classe que recebe o contexto do banco de dados por injeção de dependência
        {
            _context = context; // Atribui a instância do contexto à variável privada
        }

        public string CarrinhoCompraId { get; set; } // Propriedade que armazena o ID do carrinho de compras
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; } // Propriedade que armazena a lista de itens no carrinho de compras

        public static CarrinhoCompra GetCarrinho(IServiceProvider services) // Método estático que retorna uma instância do carrinho de compras
        {
            // Define uma sessão
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // Obtém um serviço do tipo do nosso contexto de banco de dados
            var context = services.GetService<AppDbContext>();

            // Obtém ou gera o ID do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // Atribui o ID do carrinho na sessão
            session.SetString("CarrinhoId", carrinhoId);

            // Retorna o carrinho com o contexto e o ID atribuído ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId // Define o ID do carrinho de compras
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche) // Método que adiciona um item ao carrinho de compras
        {
            // Verifica se o lanche já está no carrinho para o mesmo ID de carrinho
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null) // Se o item não estiver no carrinho
            {
                // Cria um novo item do carrinho e o adiciona ao contexto
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId, // Define o ID do carrinho de compras
                    Lanche = lanche, // Define o lanche que está sendo adicionado
                    Quantidade = 1 // Define a quantidade inicial do item
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem); // Adiciona o item ao contexto
            }
            else
            {
                // Se o item já estiver no carrinho, incrementa a quantidade
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges(); // Salva as mudanças no banco de dados
        }

        public int RemoverDoCarrinho(Lanche lanche) // Método que remove um item do carrinho de compras
        {
            // Verifica se o item do lanche existe no carrinho
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0; // Variável que armazena a quantidade local do item

            if (carrinhoCompraItem != null) // Se o item existe no carrinho
            {
                if (carrinhoCompraItem.Quantidade > 1) // Se a quantidade for maior que 1
                {
                    carrinhoCompraItem.Quantidade--; // Decrementa a quantidade
                    quantidadeLocal = carrinhoCompraItem.Quantidade; // Atualiza a quantidade local
                }
                else
                {
                    // Se a quantidade for 1 ou menos, remove o item do carrinho
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges(); // Salva as mudanças no banco de dados
            return quantidadeLocal; // Retorna a quantidade local atualizada
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens() // Método que obtém os itens do carrinho de compras
        {
            // Retorna a lista de itens no carrinho ou, se for nula, carrega os itens do banco de dados
            return CarrinhoCompraItems ??
                   (CarrinhoCompraItems =
                       _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                           .Include(s => s.Lanche) // Inclui o objeto Lanche na consulta
                           .ToList());
        }

        public void LimparCarrinho() // Método que limpa todos os itens do carrinho de compras
        {
            // Obtém todos os itens no carrinho de compras com o ID atual
            var carrinhoItens = _context.CarrinhoCompraItens
                                 .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            // Remove todos os itens do contexto
            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges(); // Salva as mudanças no banco de dados
        }

        public decimal MostarCarrinhoCompraTotal() // Método que calcula o total do carrinho de compras
        {
            // Soma o preço dos lanches multiplicado pela quantidade para todos os itens no carrinho
            var total = _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
            return total; // Retorna o total calculado
        }
    }
}
