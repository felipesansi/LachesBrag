using LachesBrag.Models;

namespace LachesBrag.Repositories.Interfaces
{
    public interface ILanchesRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchesPreferidos { get; }
        Lanche GetLanchesbyId(int lancheId);
    }
}
