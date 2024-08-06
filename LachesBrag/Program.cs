using LachesBrag.Context; // Importa o namespace do contexto da aplica��o
using LachesBrag.Repositories.Interfaces; // Importa as interfaces dos reposit�rios
using Microsoft.EntityFrameworkCore; // Importa o namespace para usar o Entity Framework Core

// Cria o construtor da aplica��o web
var builder = WebApplication.CreateBuilder(args);

// Configurar o Entity Framework com o SQL Server
// Adiciona o contexto do banco de dados como um servi�o usando o Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar servi�os ao cont�iner de inje��o de depend�ncia.
builder.Services.AddControllersWithViews(); // Adiciona o suporte para controladores e views MVC

// Registro dos reposit�rios
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>(); // Registra o reposit�rio de categoria como um servi�o transit�rio
builder.Services.AddTransient<ILanchesRepository, LanchesRepository>(); // Registra o reposit�rio de lanches como um servi�o transit�rio

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Adiciona o HttpContextAccessor como um servi�o singleton

builder.Services.AddMemoryCache(); // Adiciona o servi�o de cache em mem�ria
builder.Services.AddSession(); // Adiciona o suporte para sess�es

var app = builder.Build(); // Constroi a aplica��o

// Configurar o pipeline de requisi��o HTTP.
if (!app.Environment.IsDevelopment()) // Se n�o estiver em ambiente de desenvolvimento
{
    app.UseExceptionHandler("/Home/Error"); // Usa o manipulador de exce��es para redirecionar para a p�gina de erro
    app.UseHsts(); // Usa HTTP Strict Transport Security (HSTS) para refor�ar a seguran�a
}

app.UseHttpsRedirection(); // Redireciona as requisi��es HTTP para HTTPS
app.UseStaticFiles(); // Serve arquivos est�ticos
app.UseSession(); // Habilita o uso de sess�es
app.UseRouting(); // Habilita o roteamento

app.UseAuthorization(); // Habilita a autoriza��o

app.MapControllerRoute(
    name: "default", // Define a rota padr�o
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Padr�o de rota que mapeia para controladores e a��es

app.Run(); // Executa a aplica��o
