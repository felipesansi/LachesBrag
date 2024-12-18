using Microsoft.EntityFrameworkCore;
using LachesBrag.Context;
using LachesBrag.Repositories.Interfaces;
using LanchesBrag.Models;
using Microsoft.AspNetCore.Identity;
using LachesBrag.Service;
using ReflectionIT.Mvc.Paging;
using LachesBrag.Areas.Admin.Servicos;
using LachesBrag.Models;
using FastReport.Data;
using FastReport; // Adicione essa linha para importar o FastReport

// Cria o construtor da aplica��o web
var builder = WebApplication.CreateBuilder(args);

// Configurar o Entity Framework com o SQL Server
// Adiciona o contexto do banco de dados como um servi�o usando o Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar o Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Configura��o de requisitos para senhas de cadastro no sistema
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

// Adicionar servi�os ao cont�iner de inje��o de depend�ncia
builder.Services.AddControllersWithViews(); // Adiciona o suporte para controladores e views MVC

// Registra o servi�o do FastReport
builder.Services.AddFastReport(); // Registra o FastReport no cont�iner DI

// Registro dos reposit�rios
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>(); // Registra o reposit�rio de categoria como um servi�o transit�rio
builder.Services.AddTransient<ILanchesRepository, LanchesRepository>(); // Registra o reposit�rio de lanches como um servi�o transit�rio
builder.Services.AddTransient<IPeddidoRepository, PedidoRepository>(); // Registra o reposit�rio de pedido como um servi�o transit�rio
builder.Services.AddScoped<RelatorioVendasService>();
builder.Services.AddScoped<GraficosLancheServices>();
// Adiciona a classe SeedUserRolesInitial como um servi�o de inje��o de depend�ncia com escopo (Scoped)
builder.Services.AddScoped<ISeedUserRolesInitial, SeedUserRolesInitial>();
builder.Services.Configure<ConfigImagens>(builder.Configuration.GetSection("ConfigurationPastaImagens"));
builder.Services.AddScoped<RelatorioLanchesServices>();

// Configura a pol�tica de autoriza��o
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", politica =>
    {
        politica.RequireRole("Admin");
    });
});

// Registra o servi�o `CarrinhoCompra` no cont�iner de inje��o de depend�ncias com o ciclo de vida "Scoped"
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Adiciona o HttpContextAccessor como um servi�o singleton

// Configurando o servi�o de pagina��o com a exibi��o Bootstrap4 e nome do par�metro de p�gina como "pageindex"
builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap4";
    options.PageParameterName = "pageindex";
});

builder.Services.AddMemoryCache(); // Adiciona o servi�o de cache em mem�ria
builder.Services.AddSession(); // Adiciona o suporte para sess�es

var app = builder.Build(); // Constr�i a aplica��o

// Configurar o pipeline de requisi��o HTTP
if (!app.Environment.IsDevelopment()) // Se n�o estiver em ambiente de desenvolvimento
{
    app.UseExceptionHandler("/Home/Error"); // Usa o manipulador de exce��es para redirecionar para a p�gina de erro
    app.UseHsts(); // Usa HTTP Strict Transport Security (HSTS) para refor�ar a seguran�a
}
app.UseHttpsRedirection(); // Redireciona as requisi��es HTTP para HTTPS
app.UseStaticFiles(); // Serve arquivos est�ticos
app.UseSession(); // Habilita o uso de sess�es
app.UseFastReport(); // Habilita o uso do FastReport
app.UseRouting(); // Habilita o roteamento

app.UseAuthentication(); // Habilita a autentica��o
app.UseAuthorization(); // Habilita a autoriza��o

// Configura��o das rotas
app.MapControllerRoute(
    name: "list",  // Define o nome da rota como "list"
    pattern: "Lanche/{action=List}/{categoria?}", // Define o padr�o da URL que ser� mapeado para esta rota
    defaults: new { controller = "Lanche", action = "List" } // Define valores padr�o para os par�metros da rota
);

app.MapControllerRoute( // Rota �rea admin
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default", // Define a rota padr�o
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Padr�o de rota que mapeia para controladores e a��es

// Inicializa as roles e os users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seedUserRolesInitial = services.GetRequiredService<ISeedUserRolesInitial>();
    seedUserRolesInitial.SeedRoles();
    seedUserRolesInitial.SeedUsers();
}

app.Run(); // Executa a aplica��o
