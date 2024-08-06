using LachesBrag.Context; // Importa o namespace do contexto da aplicação
using LachesBrag.Repositories.Interfaces; // Importa as interfaces dos repositórios
using Microsoft.EntityFrameworkCore; // Importa o namespace para usar o Entity Framework Core

// Cria o construtor da aplicação web
var builder = WebApplication.CreateBuilder(args);

// Configurar o Entity Framework com o SQL Server
// Adiciona o contexto do banco de dados como um serviço usando o Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar serviços ao contêiner de injeção de dependência.
builder.Services.AddControllersWithViews(); // Adiciona o suporte para controladores e views MVC

// Registro dos repositórios
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>(); // Registra o repositório de categoria como um serviço transitório
builder.Services.AddTransient<ILanchesRepository, LanchesRepository>(); // Registra o repositório de lanches como um serviço transitório

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Adiciona o HttpContextAccessor como um serviço singleton

builder.Services.AddMemoryCache(); // Adiciona o serviço de cache em memória
builder.Services.AddSession(); // Adiciona o suporte para sessões

var app = builder.Build(); // Constroi a aplicação

// Configurar o pipeline de requisição HTTP.
if (!app.Environment.IsDevelopment()) // Se não estiver em ambiente de desenvolvimento
{
    app.UseExceptionHandler("/Home/Error"); // Usa o manipulador de exceções para redirecionar para a página de erro
    app.UseHsts(); // Usa HTTP Strict Transport Security (HSTS) para reforçar a segurança
}

app.UseHttpsRedirection(); // Redireciona as requisições HTTP para HTTPS
app.UseStaticFiles(); // Serve arquivos estáticos
app.UseSession(); // Habilita o uso de sessões
app.UseRouting(); // Habilita o roteamento

app.UseAuthorization(); // Habilita a autorização

app.MapControllerRoute(
    name: "default", // Define a rota padrão
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Padrão de rota que mapeia para controladores e ações

app.Run(); // Executa a aplicação
