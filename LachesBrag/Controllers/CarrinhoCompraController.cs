using LachesBrag.Repositories.Interfaces;
using LachesBrag.ViewModel;
using LanchesBrag.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LachesBrag.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILanchesRepository _lancheRepository;  // Declara uma variável para armazenar o repositório de lanches
        private readonly CarrinhoCompra _carrinhoCompra;  // Declara uma variável para armazenar a instância do carrinho de compras

        // Construtor do controlador que injeta o repositório de lanches e o carrinho de compras
        public CarrinhoCompraController(ILanchesRepository lancheRepository,
            CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;  // Inicializa o repositório de lanches
            _carrinhoCompra = carrinhoCompra;  // Inicializa o carrinho de compras
        }

        // Método que exibe a página principal do carrinho de compras
        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();  // Obtém os itens do carrinho de compras
            _carrinhoCompra.CarrinhoCompraItems = itens;  // Atribui os itens ao carrinho de compras

            // Cria um ViewModel para passar os dados para a view
            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,  // Atribui o carrinho de compras atual ao ViewModel
                CarrinhoCompraTotal = _carrinhoCompra.MostarCarrinhoCompraTotal()// Calcula o total do carrinho e atribui ao ViewModel
            };

            return View(carrinhoCompraVM);  // Retorna a view com o ViewModel do carrinho de compras
        }

        // Método que adiciona um item ao carrinho de compras
        [Authorize]
        public IActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
        {
            // Busca o lanche selecionado pelo ID
            var lancheSelecionado = _lancheRepository.Lanches
                                    .FirstOrDefault(p => p.LancheId == lancheId);

            if (lancheSelecionado != null)  // Se o lanche foi encontrado
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);  // Adiciona o lanche ao carrinho
            }
            return RedirectToAction("Index");  // Redireciona para a página principal do carrinho
        }

        // Método que remove um item do carrinho de compras
        [Authorize]
        public IActionResult RemoverItemDoCarrinhoCompra(int lancheId)
        {
            // Busca o lanche selecionado pelo ID
            var lancheSelecionado = _lancheRepository.Lanches
                                    .FirstOrDefault(p => p.LancheId == lancheId);

            if (lancheSelecionado != null)  // Se o lanche foi encontrado
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);  // Remove o lanche do carrinho
            }
            return RedirectToAction("Index");  // Redireciona para a página principal do carrinho
        }
    }
}
