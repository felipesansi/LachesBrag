using LachesBrag.Models;
using LachesBrag.ViewModel;
using LanchesBrag.Models;
using Microsoft.AspNetCore.Mvc;

namespace LachesBrag.Components
{
    // Este componente de visualização resume o conteúdo do carrinho de compras
    public class CarrinhoCompraResumo : ViewComponent
    {
        // Referência ao carrinho de compras injetado via dependência
        private readonly CarrinhoCompra _carrinhoCompra;

        // Construtor que inicializa a instância do carrinho de compras
        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
        }

        // Método Invoke é chamado quando o componente de visualização é renderizado
        public IViewComponentResult Invoke()
        {
            // Obtém os itens do carrinho de compras atual
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
          

            // Atualiza os itens do carrinho de compras
            _carrinhoCompra.CarrinhoCompraItems = itens;

            // Cria uma instância do ViewModel com os dados do carrinho de compras
            var carrinho_view_model = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                TotalCompraItens = _carrinhoCompra.TotalItens()
            };

            // Retorna a visualização com o ViewModel populado
            return View(carrinho_view_model);
        }
    }
}
