using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LachesBrag.Models
{
    [Table("CarrinoCompraItens")]
    public class CarrinhoCompraItem
    {
        [Key]
        public int CarrinhoCompraItemId { get; set; } // chave primária
         public Lanches Lanche { get; set; } // cheve estrangeira

        public int Quantidade { get; set; }

        [MaxLength(200)]
        public int CarrinhoCompraId { get; set; }
    }
}
