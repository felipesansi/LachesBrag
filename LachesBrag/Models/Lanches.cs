using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LachesBrag.Models
{
    [Table ("Lanches")]
    public class Lanches
    {
        [Key]
        public int LancheId { get; set; } // Chave primaria lanches

        [Required(ErrorMessage = "O campo de Nome do lache é obrigatório ")]
        [Display (Name = "Informe o Nome do lanche")]
        [StringLength (80, MinimumLength = 10, ErrorMessage = "O {0} deve ter o minimo {1} e o maximo {2} caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo de Descrição do lache é obrigatório ")]
        [Display(Name = "Informe a descrição do lanche")]
        [MaxLength(200, ErrorMessage = "Descrição dever ter no maximo {1} caracteres")]
        [MinLength(20, ErrorMessage = "Descrição dever ter no minimo {1} caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "O campo de Descrição detalhada do lache é obrigatório ")]
        [Display(Name = "Informe a descrição detalhada do lanche")]
        [MaxLength(200, ErrorMessage = "Descrição dever ter no maximo {1} caracteres")]
        [MinLength(20, ErrorMessage = "Descrição dever ter no minimo {1} caracteres")]
        public string DescricacaoDetalhada { get; set; }

        [Required(ErrorMessage = "O campo de Preço  do lache é obrigatório ")]
        [Display(Name = "Informe o Preço do lanche")]
        [Column (TypeName = "decimal(10,2)")]
        [Range (1,99.99 , ErrorMessage = "O preço deve estar entre 1 e 99, 99 reais")]
        public decimal Preco { get; set; }
        [Display(Name = "Caminho Imagem Normal")]
        [StringLength (200, ErrorMessage = "O {0} deve ter no maximo {1} caracteres")]
        public string ImagemUrl { get; set; }
        [Display(Name = "Caminho Imagem  Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no maximo {1} caracteres")]
        public string ImagemThumbnailUrl { get; set; }
        [Display(Name ="Preferido")]
        public bool LanchePreferido { get; set; }
        [Display(Name = "Estoque")]
        public bool LancheEmEstoque { get; set; }

        public int CategoriaId { get; set; }  // relacionamentos entre lanches e categoria
        public virtual Categoria Categoria { get; set; }  // relacionamentos entre lanches e categoria
    }
}