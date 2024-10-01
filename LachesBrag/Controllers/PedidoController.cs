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
            return View();
        }
    }
}
