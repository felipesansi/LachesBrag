using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LachesBrag.Models
{
    
    public class Lanches
    {
        [Key]
        public int LancheId { get; set; } // Chave primaria lanches
        [Required (ErrorMessage ="Campo obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string DescricaoCurta { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string DescricacaoDetalhada { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public decimal Preco { get; set; }
        public string ImagemUrl { get; set; }
        public string ImagemThumbnailUrl { get; set; }
        public bool LanchePreferido { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public bool LancheEmEstoque { get; set; }

        public int CategoriaId { get; set; }  // relacionamentos entre lanches e categoria
        public virtual Categoria Categoria { get; set; }  // relacionamentos entre lanches e categoria
    }
}