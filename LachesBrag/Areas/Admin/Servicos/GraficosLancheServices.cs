using LachesBrag.Context;
using LachesBrag.Models;

namespace LachesBrag.Areas.Admin.Servicos
{
    public class GraficosLancheServices
    {
        private readonly AppDbContext context;

        public GraficosLancheServices(AppDbContext context)
        {
            this.context = context;
        }
        public List<LanchesGraficos> ListarDados(int dias = 360)
        {
            var data = DateTime.Now.AddDays(-dias);
            var consulta_lanches = (from pd in context.PedidosDetalhe
                                    join l in context.Lanches on pd.LancheId equals l.LancheId
                                    where
                                    pd.Pedido.PedidoEnviado >= data
                                    group pd by new { pd.LancheId, l.Nome }
                                    into g
                                    select new
                                    {
                                        lancheNome = g.Key.Nome,
                                        lanchesQuantidade = g.Sum(l => l.Quantidade),
                                        lancheValorTotal = g.Sum(g => g.Preco * g.Quantidade),
                                    });
            var lista = new List<LanchesGraficos>();

            foreach(var item in consulta_lanches)
            {
                var l = new LanchesGraficos();
                l.NomeLanche = item.lancheNome;
                l.QuantidadeTotalVendida = item.lanchesQuantidade;
                l.ValorTotalVendido = item.lancheValorTotal;
                lista.Add(l);
            }
            return lista;
        }
    }
}
