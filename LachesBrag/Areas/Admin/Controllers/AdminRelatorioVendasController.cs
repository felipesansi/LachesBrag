using LachesBrag.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LachesBrag.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendasService _relatorioVendasService;

        public AdminRelatorioVendasController(RelatorioVendasService relatorioVendasService)
        {
            _relatorioVendasService = relatorioVendasService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? dataMin, DateTime? dataMax)
        {
            if (!dataMin.HasValue)
            {
                dataMin = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!dataMax.HasValue)
            {
                dataMax = new DateTime(DateTime.Now.Year, 12, 31);
            }

            if (dataMin > dataMax)
            {
                TempData["Error"] = "A data inicial não pode ser maior que a data final.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["dataMin"] = dataMin.Value.ToString("yyyy-MM-dd");
            ViewData["dataMax"] = dataMax.Value.ToString("yyyy-MM-dd");

            try
            {
                var (pedidos, totalLanches, valorTotalVendas) = await _relatorioVendasService.FindByDateAsync(dataMin, dataMax);

                ViewData["totalLanches"] = totalLanches;
                ViewData["valorTotalVendas"] = valorTotalVendas;

                return View(pedidos);
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocorreu um erro ao gerar o relatório. Tente novamente mais tarde.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
