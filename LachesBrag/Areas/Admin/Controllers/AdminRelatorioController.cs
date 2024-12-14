using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using LachesBrag.Areas.Admin.FastReportUltil;
using LachesBrag.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace LachesBrag.Areas.Admin.Controllers
{
    [Area("Admin")] // Indica que este controlador pertence à área "Admin", uma parte da aplicação que pode ter rotas e recursos separados.
    public class AdminRelatorioController : Controller // Define a classe que herda de "Controller", permitindo a utilização de ações MVC.
    {
        private readonly IWebHostEnvironment _webHostEnvironment; // Variável que permite acessar o ambiente de hospedagem da aplicação, como a raiz do servidor.
        private readonly RelatorioLanchesServices _relatorioLanchesServices; // Variável para acessar os serviços responsáveis pela lógica de relatórios de lanches.

        // Construtor que injeta as dependências necessárias
        public AdminRelatorioController(IWebHostEnvironment webHostEnvironment, RelatorioLanchesServices relatorioLanchesServices)
        {
            _webHostEnvironment = webHostEnvironment; // Atribui o parâmetro para acessar o ambiente de hospedagem à variável de classe.
            _relatorioLanchesServices = relatorioLanchesServices; // Atribui o serviço que fornece os dados de lanches à variável de classe.
        }

        // Método que gera o relatório
        public async Task<IActionResult> GerarRelatorio()
        {
            var fastReport_web = new WebReport(); // Cria uma instância do objeto WebReport, utilizado para exibir o relatório na web.
            FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection)); // Registra o tipo de conexão com o banco de dados SQL Server.

            var fastReport_conect = new MsSqlDataConnection(); // Cria uma instância de conexão com o banco de dados SQL Server.
            fastReport_web.Report.Dictionary.Connections.Add(fastReport_conect); // Adiciona a conexão com o banco de dados ao dicionário de fontes de dados do relatório.

            fastReport_web.Report.Load(Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/reports", "lanchesCategoria.frx"));
            // Carrega o modelo de relatório (.frx), que contém o layout e a estrutura do relatório, de um diretório específico no servidor.

            // Recupera os dados de lanches e categorias dos serviços, e os prepara para serem usados no relatório
            var lanches = HelperFastReport.GetTable(await _relatorioLanchesServices.MostrarLanchesRelatorio(), "LanchesReports");
            var categorias = HelperFastReport.GetTable(await _relatorioLanchesServices.MostrarCategoriasRelatorio(), "CategoriasReports");

            fastReport_web.Report.RegisterData(lanches, "LanchesReports"); // Registra os dados de lanches no relatório, associando-os ao nome "LanchesReports".
            fastReport_web.Report.RegisterData(categorias, "CategoriasReports"); // Registra os dados de categorias no relatório, associando-os ao nome "CategoriasReports".

            return View(fastReport_web); // Retorna a visualização do relatório gerado ao cliente, com os dados registrados.
        }

        public async Task<IActionResult> GerarRelatorioPDF()
        {
            var fastReport_web = new WebReport(); // Cria uma instância do objeto WebReport, utilizado para exibir o relatório na web.
            FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection)); // Registra o tipo de conexão com o banco de dados SQL Server.

            var fastReport_conect = new MsSqlDataConnection(); // Cria uma instância de conexão com o banco de dados SQL Server.
            fastReport_web.Report.Dictionary.Connections.Add(fastReport_conect); // Adiciona a conexão com o banco de dados ao dicionário de fontes de dados do relatório.

            fastReport_web.Report.Load(Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/reports", "lanchesCategoria.frx"));
            // Carrega o modelo de relatório (.frx), que contém o layout e a estrutura do relatório, de um diretório específico no servidor.

            // Recupera os dados de lanches e categorias dos serviços, e os prepara para serem usados no relatório
            var lanches = HelperFastReport.GetTable(await _relatorioLanchesServices.MostrarLanchesRelatorio(), "LanchesReports");
            var categorias = HelperFastReport.GetTable(await _relatorioLanchesServices.MostrarCategoriasRelatorio(), "CategoriasReports");

            fastReport_web.Report.RegisterData(lanches, "LanchesReports"); // Registra os dados de lanches no relatório, associando-os ao nome "LanchesReports".
            fastReport_web.Report.RegisterData(categorias, "CategoriasReports"); // Registra os dados de categorias no relatório, associando-os ao nome "CategoriasReports".

            fastReport_web.Report.Prepare();
            Stream stream = new MemoryStream();
            fastReport_web.Report.Export(new PDFSimpleExport(), stream);
            stream.Position = 0;
            string data = DateTime.Now.ToString("dd/MM/yyyy");
            string titulo = "Relatório Lanches " + data + ".pdf";
            return File(stream, "application/pdf", titulo);
        }
    }
}
