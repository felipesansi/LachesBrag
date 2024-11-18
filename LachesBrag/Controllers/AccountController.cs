using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LachesBrag.ViewModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace LachesBrag.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _UserManager;
        private readonly SignInManager<IdentityUser> _SignInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var usuario = await _UserManager.FindByNameAsync(loginVM.UserName);

            if (usuario != null)
            {
                var resultado = await _SignInManager.PasswordSignInAsync(usuario, loginVM.Password, false, false);
                if (resultado.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Falha ao realizar login");
            return View(loginVM);
        }
      
        public IActionResult Register() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // gera tokens contra ataques 
        public async Task<IActionResult> Register(LoginViewModel regitroVM)
        {
            if (ModelState.IsValid)
            {
                var usuario = new IdentityUser { UserName = regitroVM.UserName };
                var resultado = await _UserManager.CreateAsync(usuario,regitroVM.Password); 
              
                
                if (resultado.Succeeded)
                {

                    await _UserManager.AddToRoleAsync(usuario, "Member");
                   return RedirectToAction("Login", "Account");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao realizar o registro de usuário");
                }

              

            }
            return View(regitroVM);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Limpar a sessão
            HttpContext.Session.Clear();

            // Fazer sign-out do esquema de autenticação
            await _SignInManager.SignOutAsync();

            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
