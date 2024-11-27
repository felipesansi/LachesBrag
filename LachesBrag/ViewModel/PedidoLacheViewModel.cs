using LachesBrag.Models;

namespace LachesBrag.ViewModel
{
    public class PedidoLacheViewModel
    {
        public Pedido pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhe { get; set; }
    }
}
