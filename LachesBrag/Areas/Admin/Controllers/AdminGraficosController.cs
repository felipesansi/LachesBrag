using LachesBrag.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace LachesBrag.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGraficosController : Controller
    {
        private readonly GraficosLancheServices _graficosLancheServices;

        public AdminGraficosController(GraficosLancheServices graficosLancheServices)
        {
            _graficosLancheServices = graficosLancheServices ?? throw new ArgumentNullException(nameof(graficosLancheServices));
        }
        public JsonResult VendasLanches(int dias)
        {
            var vendasTotaisLanches = _graficosLancheServices.ListarDados(dias);
           
            return Json(vendasTotaisLanches);
        }
        public IActionResult VndasMensal()
        {
            return View();
        }

        public IActionResult VendasSemanal()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
