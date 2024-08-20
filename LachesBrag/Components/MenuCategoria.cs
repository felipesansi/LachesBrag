using LachesBrag.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LachesBrag.Components
{
    public class MenuCategoria : ViewComponent
    {
        private readonly ICategoriaRepository categoriaRepository;
        public MenuCategoria(ICategoriaRepository categoriaRepository)
        {
            this.categoriaRepository = categoriaRepository;
        }
        public IViewComponentResult Invoke()
        {
            var categorias = categoriaRepository.Categorias.OrderBy(cat => cat.CategoriaNome);
            return View(categorias);
        }
    }
}
