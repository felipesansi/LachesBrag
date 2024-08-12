using LachesBrag.Repositories.Interfaces;  
using LachesBrag.ViewModel;
using LanchesMac.Models; 
using Microsoft.AspNetCore.Mvc;  

namespace LachesBrag.Controllers
{
    public class CarrinhoCompraController : Controller 
    {
        private readonly ILanchesRepository _lanchesRepository;  // Declara uma variável para armazenar a instância do repositório de lanches
        private readonly CarrinhoCompra _carrinhoCompra;  // Declara uma variável para armazenar a instância do CarrinhoCompra

        public CarrinhoCompraController(ILanchesRepository lanchesRepository, CarrinhoCompra carrinhoCompra)  // Construtor da classe que recebe dependências por injeção
        {
            _lanchesRepository = lanchesRepository;  // Atribui a instância do repositório à variável privada
            _carrinhoCompra = carrinhoCompra;  // Atribui a instância do CarrinhoCompra à variável privada
        }

        public IActionResult Index()  // Método que trata a ação "Index" do controlador
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();  // Obtém os itens do carrinho de compras
            _carrinhoCompra.CarrinhoCompraItems = itens;  // Atribui os itens obtidos à propriedade CarrinhoCompraItems

            var carrinhoCompraViewModel = new CarrinhoCompraViewModel  // Cria uma instância do ViewModel para o Carrinho de Compras
            {
                CarrinhoCompra = _carrinhoCompra,  // Atribui o carrinho de compras atual ao ViewModel
                TotalCompraItens = _carrinhoCompra.TotalItens()  // Calcula o total de itens no carrinho e atribui ao ViewModel
            };

            return View(carrinhoCompraViewModel);  // Retorna a view com o ViewModel do Carrinho de Compras
        }
        public IActionResult AdicionarItemNoCarrinho(int id_lanche) 
        { 
            var lancheSelecionado = _lanchesRepository.Lanches.LastOrDefault(p=>p.LancheId == id_lanche);
            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoverItemCarrinho (int id_lanche) 
        {
             var lancheSelecionado = _lanchesRepository.Lanches.LastOrDefault(p => p.LancheId == id_lanche);
            if(lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverItem(lancheSelecionado);
         
            }
            return RedirectToAction("Index");
        }
    }
}
