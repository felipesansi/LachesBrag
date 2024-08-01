using LachesBrag.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LachesBrag.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILanchesRepository _LannchesRepository;

        public LancheController(ILanchesRepository lannchesRepository)
        {
            _LannchesRepository = lannchesRepository;
        }

        public IActionResult List()
        {
            ViewData["Titulo"] = "Todos os Lanches";
            ViewData["Data"] = DateTime.Now;

            var lanches = _LannchesRepository.Lanches;
            

            ViewBag.TotalLaches = lanches.Count();
            ViewBag.Total = "Total de Lanches : ";
                return View(lanches);
        }
    }
}
