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
            var lanches = _LannchesRepository.Lanches;
            return View(lanches);
        }
    }
}
