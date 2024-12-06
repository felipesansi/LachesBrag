using LachesBrag.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")] // Define que esse controller pertence à área "Admin".
    [Authorize(Roles = "Admin")] // Restringe o acesso apenas para usuários com a role "Admin".
    public class AdminImagensController : Controller
    {
        private readonly ConfigImagens _myConfig; // Configurações relacionadas às imagens.
        private readonly IWebHostEnvironment _hostingEnvironment; // Ambiente de hospedagem para obter caminhos físicos do servidor.

        public AdminImagensController(IWebHostEnvironment hostingEnvironment,
            IOptions<ConfigImagens> myConfiguration)
        {
            _hostingEnvironment = hostingEnvironment; // Inicializa o ambiente de hospedagem.
            _myConfig = myConfiguration.Value; // Obtém as configurações de imagens do arquivo de configuração.
        }

        public IActionResult Index()
        {
            return View(); // Retorna a view principal para gerenciamento de imagens.
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            // Verifica se nenhum arquivo foi enviado.
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)"; // Define uma mensagem de erro.
                return View(ViewData); // Retorna a view com a mensagem de erro.
            }

            // Limita o número máximo de arquivos que podem ser enviados (10).
            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite"; // Define uma mensagem de erro.
                return View(ViewData); // Retorna a view com a mensagem de erro.
            }

            // Calcula o tamanho total dos arquivos enviados.
            long size = files.Sum(f => f.Length);

            // Lista para armazenar os caminhos completos dos arquivos salvos.
            var filePathsName = new List<string>();

            // Combina o caminho físico do servidor com a pasta de imagens configurada.
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            // Itera sobre cada arquivo enviado.
            foreach (var formFile in files)
            {
                // Verifica se o arquivo possui uma extensão válida (jpg, gif ou png).
                if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif") ||
                    formFile.FileName.Contains(".png"))
                {
                    // Cria o caminho completo do arquivo, combinando o diretório e o nome do arquivo.
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                    // Adiciona o caminho completo do arquivo à lista.
                    filePathsName.Add(fileNameWithPath);

                    // Abre um stream de arquivo para salvar o conteúdo enviado.
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream); // Copia o conteúdo do arquivo enviado para o servidor.
                    }
                }
            }

            // Monta uma mensagem de sucesso com o número de arquivos enviados e o tamanho total.
            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, " +
                                    $"com tamanho total de : {size} bytes";

            // Define os caminhos dos arquivos enviados na ViewBag para exibição na view.
            ViewBag.Arquivos = filePathsName;

            // Retorna a view com as informações de resultado.
            return View(ViewData);
        }
        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);
          
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);

            FileInfo[] files = directoryInfo.GetFiles();
         
            model = new FileManagerModel();
        
            model.PathImagemProduto= _myConfig.NomePastaImagensProdutos;
       
            if (files.Length > 0)
            {
               
                model.Files = directoryInfo.GetFiles();
            }
            else
            {
                ViewData["Erro"] = "Error: Não existe nenhum arquivo com esse nome"; // define mensagem de erro
                return View(ViewData); // Retorna a view com a mensagem de erro.
            }
            return View(model);
        }
    }
}
