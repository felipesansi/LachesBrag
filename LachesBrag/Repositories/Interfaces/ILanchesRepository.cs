using LachesBrag.Models;

namespace LachesBrag.Repositories.Interfaces
{
    public interface ILanchesRepository
    {
        IEnumerable<Lanches> Lanches { get; }
        IEnumerable<Lanches> LanchesPreferidos { get; }
        Lanches GetLanchesbyId(int lancheId);
    }
}
