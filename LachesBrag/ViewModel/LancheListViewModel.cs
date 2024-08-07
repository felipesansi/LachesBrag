using LachesBrag.Models;

namespace LachesBrag.ViewModel
{
    /// <summary>
    /// A classe LancheListViewModel é uma ViewModel que segue o padrão MVVM.
    /// Ela serve como um intermediário entre a View e o Model, encapsulando
    /// os dados e a lógica de apresentação necessária para a interface do usuário
    /// exibir uma lista de lanches e a categoria atual.
    /// 
    /// Responsabilidades da LancheListViewModel:
    /// 1. Encapsulamento de Dados: 
    ///    - Armazena uma coleção de objetos do tipo Lanches, que representa
    ///      a lista de lanches a serem exibidos pela View.
    ///    - Mantém o estado da categoria atual para que a View possa exibir
    ///      informações relevantes.
    /// 
    /// 2. Facilitação do Bind de Dados:
    ///    - Exponibiliza os dados do Model em propriedades que são facilmente
    ///      vinculáveis aos elementos da interface gráfica.
    /// 
    /// 3. Separação de Responsabilidades:
    ///    - Separa a lógica de apresentação da lógica de negócios, permitindo
    ///      que a View se concentre apenas em como os dados devem ser exibidos.
    ///    - Facilita a reutilização do código e simplifica a manutenção.
    /// 
    /// 4. Testabilidade:
    ///    - Permite testes unitários eficientes, já que a lógica de apresentação
    ///      pode ser testada sem a necessidade de uma interface gráfica.
    /// 
    /// Propriedades:
    /// - IEnumerable<Lanches> lanches: Coleção de lanches que será exibida na View.
    /// - string categoriaAtual: Armazena o nome da categoria atual para exibição.
    /// </summary>
    public class LancheListViewModel
    {
        /// <summary>
        /// Coleção de lanches a serem exibidos na View.
        /// </summary>
        public IEnumerable<Lanche> lanches { get; set; }

        /// <summary>
        /// Nome da categoria atual sendo exibida na View.
        /// </summary>
        public string categoriaAtual { get; set; }
    }
}
