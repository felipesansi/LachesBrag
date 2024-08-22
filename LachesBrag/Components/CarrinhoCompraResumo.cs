using LachesBrag.Models;
using LachesBrag.ViewModel;
using LanchesBrag.Models;
using Microsoft.AspNetCore.Mvc;

namespace LachesBrag.Components
{
    // Este componente de visualização resume o conteúdo do carrinho de compras
    public class CarrinhoCompraResumo : ViewComponent
    {
        private readonly CarrinhoCompra _carrinhoCompra;  // Declara uma variável para armazenar a instância do carrinho de compras

        // Construtor que injeta a dependência do carrinho de compras
        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;  // Inicializa o carrinho de compras
        }

        // Método que é invocado quando o componente é renderizado
        public IViewComponentResult Invoke()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();  // Obtém os itens do carrinho de compras

            _carrinhoCompra.CarrinhoCompraItems = itens;  // Atribui os itens obtidos ao carrinho de compras

            // Cria um ViewModel para passar os dados para a view do componente
            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,  // Atribui o carrinho de compras atual ao ViewModel
                CarrinhoCompraTotal = _carrinhoCompra.MostarCarrinhoCompraTotal() // Calcula o total do carrinho e atribui ao ViewModel
            };

            return View(carrinhoCompraVM);  // Retorna a view com o ViewModel do carrinho de compras
        }
    }
}
