// Classe que representa o carrinho de compras
using LachesBrag.Context;
using LachesBrag.Migrations;

public class CarrinhoCompra
{
    private readonly AppDbContext _context; // Instância do contexto do banco de dados

    // Construtor que recebe o contexto do banco de dados como dependência
    public CarrinhoCompra(AppDbContext context)
    {
        _context = context;
    }

    // Propriedade que representa o ID do carrinho de compras
    public string CarrinhoCompraId { get; set; }

    // Lista de itens do carrinho de compras
    public List<CarrinhoCompraItem> carrinhoCompraItens { get; set; }

    // Método estático para criar uma instância de CarrinhoCompra
    public static CarrinhoCompra GetCarrinho(IServiceProvider service)
    {
        // Obtém o serviço IHttpContextAccessor para acessar o contexto HTTP
        ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        // Obtém o contexto do banco de dados do serviço
        var context = service.GetRequiredService<AppDbContext>();

        // Obtém o ID do carrinho da sessão, ou gera um novo GUID se não existir
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        // Armazena o ID do carrinho na sessão
        session.SetString("CarrinhoId", carrinhoId);

        // Cria uma nova instância de CarrinhoCompra com o contexto do banco de dados
        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId
        };
    }
}
