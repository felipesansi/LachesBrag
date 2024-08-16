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
                if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanches
                        .Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
                        .OrderBy(l => l.Nome);
                    categoriaAtual = categoria;
                }
                else if (string.Equals("Natural", categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanches
                       .Where(l => l.Categoria.CategoriaNome.Equals("Natural"))
                       .OrderBy(l => l.Nome);
                    categoriaAtual = categoria;
                }
                else
                {
                    lanches = Enumerable.Empty<Lanche>();
                    categoriaAtual = "Esta Categoria Não existe no Sistema ";
                }
              
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                lanches = lanches,
                categoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }

    }
}
