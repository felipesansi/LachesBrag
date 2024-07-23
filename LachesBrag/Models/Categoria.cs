using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LachesBrag.Models
{
    [Table ("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo de Nome é obrigatório ")]
        [StringLength(100,ErrorMessage ="O tamanho maximo é de 100 caracteres")]
        [Display (Name = "Nome")]
        public string CategoriaNome { get; set; }
      
        [Required(ErrorMessage = "O campo de Descrição é obrigatório ")]
        [StringLength(200, ErrorMessage = "O tamanho maximo é de 200 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public List<Lanches> Lanches { get; set; } // relacionamento com lanches

    }
}
