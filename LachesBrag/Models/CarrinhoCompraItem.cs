using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LachesBrag.Models
{
    [Table("CarrinhoCompraItens")]
    public class CarrinhoCompraItem
    {
        [Key]
        public int CarrinhoCompraItemId { get; set; } // chave primária

        public Lanche Lanche { get; set; } // chave estrangeira

        public int Quantidade { get; set; }

        [MaxLength(200)]
        public string CarrinhoCompraId { get; set; } 
    }
}
