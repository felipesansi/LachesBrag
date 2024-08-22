using LachesBrag.Models;
using LachesBrag.Repositories.Interfaces;
using LachesBrag.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILanchesRepository _lancheRepository;
        public LancheController(ILanchesRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals(categoria)).OrderBy(l => l.Nome);
                categoriaAtual = categoria;
            }


            var lanchesListViewModel = new LancheListViewModel
            {
                lanches = lanches,
                categoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }
        public IActionResult Detalhes(int lancheId)
        {
             var lanche =_lancheRepository.Lanches.FirstOrDefault(l => l.LancheId== lancheId);
            return View(lanche);
        }

    }
}
