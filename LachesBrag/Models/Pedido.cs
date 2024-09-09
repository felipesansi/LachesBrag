using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LachesBrag.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }

        [Display(Name = "Informe seu Nome")]
        [Required (ErrorMessage = "Campo obrigatório")]
        [StringLength(999)]

        public string Nome { get; set; }

        [Display(Name = "Informe seu Sobrenome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(999)]
        public string SobreNome { get; set; }
        [Display(Name = "Informe seu Endereço 1")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(999)]
        public string Endereco1 { get; set; }
        [Display(Name = "Informe seu Endereço 2")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(999)]
        public string Endereco2 { get; set; }
        [Display(Name = "Informe seu Estado")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(999)]
        public string Estado {  get; set; }
        [Display(Name = "Informe sua Cidade")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(999)]

        public string Cidade { get; set; }
        [Display(Name = "Informe seu Telefone")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }
        [Display(Name = "Informe seu E-mail")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email inválido")]
        [StringLength (50)]
        public string Email { get; set; }
      
        [Column (TypeName ="decimal(18,2)")]
        [ScaffoldColumn(false)]
        [Display(Name ="Total do Pedido")]
        public decimal PedidoTotal { get; set; }

        [Display(Name = "Total itens pedido")]
        [ScaffoldColumn(false)]
       
        public int TotalItensPedidos { get; set; }
        public DateTime PedidoEnviado { get; set; }
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]

        public DateTime? PedididoEntregue { get; set; }
        public List<PedidoDetalhe>  PedidoItens { get; set; }
    }
}
