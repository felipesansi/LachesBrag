using Microsoft.AspNetCore.Mvc;
using LachesBrag.Areas.Admin.Servicos;

namespace LachesBrag.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendasSimples _relatorioVendasSimples;

        public AdminRelatorioVendasController(RelatorioVendasSimples relatorioVendasSimples)
        {
            _relatorioVendasSimples = relatorioVendasSimples;
        }

        public async Task<IActionResult> RelatorioVendas(DateTime? dataMax, DateTime? dataMin)
        {
            if (!dataMax.HasValue)
            {
                dataMax = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!dataMin.HasValue)
            {
                dataMin = new DateTime(DateTime.Now.Year, 1, 1);
            }

            ViewData["dataMax"] = dataMax.Value.ToString("yyyy/MM/dd");
            ViewData["dataMin"] = dataMin.Value.ToString("yyyy/MM/dd");

            var resultado = await _relatorioVendasSimples.FindByAsync(dataMax, dataMin);

            return View(resultado);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
