using LachesBrag.Models;

namespace LachesBrag.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; } // lista de dados de model Categoria
    }
}
