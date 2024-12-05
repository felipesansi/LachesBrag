using LachesBrag.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
namespace LachesBrag.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles ="Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigImagens _myConfig;
        private readonly IWebHostEnvironment _env;

        public AdminImagensController(IWebHostEnvironment env,IOptions<ConfigImagens> myconfiguration)
        {
                 _env = env;
                _myConfig = myconfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
