using LachesBrag.Models;
using LachesBrag.Repositories.Interfaces;
using LanchesBrag.Models;
using Microsoft.AspNetCore.Mvc;

namespace LachesBrag.Controllers
{
    public class PedidoController : Controller
    {

        private readonly IPeddidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPeddidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {

         int totalPedido = 0;
         decimal precoTotal = 0.0m;

            // OBTER ITENS DO CARRINHO

            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();

            _carrinhoCompra.CarrinhoCompraItems = items;
            // 
            if (_carrinhoCompra.CarrinhoCompraItems.Count ==0)
            {
                ModelState.AddModelError("","Seu carrinho esta vazio ");
            }
           // 
            foreach( var item in items)
            {
                totalPedido = +item.Quantidade;
                precoTotal =+ item.Quantidade * item.Lanche.Preco;
            }
           
            // se o carrinho estiver compras
   
            if (ModelState.IsValid)
            {
                // Criar pedido
               
                _pedidoRepository.Criar_pediddo(pedido);
                
                // Mensagem de sucesso com viewBag
                ViewBag.sucesso_checkeout = "Obrigado pelo seu peddo!";
                ViewBag.totalPedido = _carrinhoCompra.MostarCarrinhoCompraTotal();
               
                // Limpar carrinho após 
                _carrinhoCompra.LimparCarrinho();

                // 
                return View("~/Views/Pedido/CheckoutCompletto.cshtml", pedido);

            }
            return View(pedido);

        }
    }
}
