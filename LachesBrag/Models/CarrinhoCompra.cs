using LachesBrag.Context;
using LachesBrag.Models;
using Microsoft.EntityFrameworkCore;


namespace LanchesMac.Models
{
   
    public class CarrinhoCompra
    {
        // Campo privado que mantém uma referência ao contexto do banco de dados
        private readonly AppDbContext _context;

        // Construtor da classe que aceita um contexto de banco de dados como argumento
        public CarrinhoCompra(AppDbContext context)
        {
            _context = context; // Inicializa o contexto do banco de dados
        }

        // Propriedade que armazena o ID do carrinho de compras, usado para identificar o carrinho de um usuário
        public string CarrinhoCompraId { get; set; }

        // Lista de itens no carrinho de compras
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        // Método estático para obter ou criar um carrinho de compras usando o provedor de serviços
        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // Obtém a sessão HTTP atual usando o IHttpContextAccessor
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // Obtém uma instância do AppDbContext a partir do provedor de serviços
            var context = services.GetService<AppDbContext>();

            // Tenta obter o ID do carrinho de compras da sessão do usuário
            // Se não existir, gera um novo GUID e o converte para string
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // Armazena o ID do carrinho na sessão para uso futuro
            session.SetString("CarrinhoId", carrinhoId);

            // Retorna uma nova instância de CarrinhoCompra com o contexto e ID do carrinho
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        // Método para adicionar um lanche ao carrinho de compras
        public void AdicionarAoCarrinho(Lanche lanche)
        {
            // Busca um item no carrinho que já contenha o lanche especificado
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                     s => s.Lanche.LancheId == lanche.LancheId &&
                     s.CarrinhoCompraId == CarrinhoCompraId);

            // Se o item não existir, cria um novo item de carrinho
            if (carrinhoCompraItem == null)
            {
                // Cria uma nova instância de CarrinhoCompraItem e o adiciona ao contexto
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1 // Inicializa a quantidade como 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                // Se o item já existir no carrinho, incrementa a quantidade
                carrinhoCompraItem.Quantidade++;
            }

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public int RemoverItem(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                     s => s.Lanche.LancheId == lanche.LancheId &&
                     s.CarrinhoCompraId == CarrinhoCompraId);
             
            var quantidade_local = 0;

            if (carrinhoCompraItem !=null) // se existir lanche no carrinho
            {
                if (carrinhoCompraItem.Quantidade>1) //se a qtdade for maior que 1
                {

                    carrinhoCompraItem.Quantidade--; // diminua a quantidade
                    quantidade_local = carrinhoCompraItem.Quantidade; //pesse para quantidade_local
                }
                else
                {
                        _context.CarrinhoCompraItens.Remove(carrinhoCompraItem); // se for igual 1 remova do carrinho
                }
           
              

            }
            _context.SaveChanges();     // salve as alterações 
            return quantidade_local;// retoorne o valor de quantidade_local

        }
    }
}
