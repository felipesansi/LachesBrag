using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LachesBrag.Models
{
    
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [Required (ErrorMessage = "Campo Obrigatório")]
      
        
        public string CategoriaNome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]

        public string Descricao { get; set; }
        public List<Lanches> Lanches { get; set; } // relacionamento com lanches

    }
}
