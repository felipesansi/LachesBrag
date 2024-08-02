using LachesBrag.Repositories.Interfaces;
using LachesBrag.ViewModel;
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
          

            //            var lanches = _LannchesRepository.Lanches;
            //                return View(lanches);
            var lancheslistViewModel = new LancheListViewModel();
            lancheslistViewModel.lanches = _LannchesRepository.Lanches;
            lancheslistViewModel.categoriaAtual = "Categoria Atual";
            
            return View(lancheslistViewModel);
        }
    }
}
