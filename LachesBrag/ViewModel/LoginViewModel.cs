using System.ComponentModel.DataAnnotations;

namespace LachesBrag.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Informe o nome de usuário") ]
        [Display (Name ="Usuario")] 
        public string UserName { get; set; }
        [Required(ErrorMessage = "Informe o nome de usuário")]
        [DataType (DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
