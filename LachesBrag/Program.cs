using Microsoft.EntityFrameworkCore;
using LachesBrag.Context;
using LachesBrag.Repositories.Interfaces;
using LanchesBrag.Models;
using Microsoft.AspNetCore.Identity;
using LachesBrag.Service;
using ReflectionIT.Mvc.Paging;

// Cria o construtor da aplicação web
var builder = WebApplication.CreateBuilder(args);

// Configurar o Entity Framework com o SQL Server
// Adiciona o contexto do banco de dados como um serviço usando o Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar o Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Configuração de requisitos para senhas de cadastro no sistema
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

// Adicionar serviços ao contêiner de injeção de dependência
builder.Services.AddControllersWithViews(); // Adiciona o suporte para controladores e views MVC

// Registro dos repositórios
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>(); // Registra o repositório de categoria como um serviço transitório
builder.Services.AddTransient<ILanchesRepository, LanchesRepository>(); // Registra o repositório de lanches como um serviço transitório
builder.Services.AddTransient<IPeddidoRepository, PedidoRepository>(); // Registra o repositório de pedido como um serviço transitório

// Adiciona a classe SeedUserRolesInitial como um serviço de injeção de dependência com escopo (Scoped)
builder.Services.AddScoped<ISeedUserRolesInitial, SeedUserRolesInitial>();

// Configura a política de autorização
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", politica =>
    {
        politica.RequireRole("Admin");
    });
});

// Registra o serviço `CarrinhoCompra` no contêiner de injeção de dependências com o ciclo de vida "Scoped"
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Adiciona o HttpContextAccessor como um serviço singleton

builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap4";
    options.PageParameterName = "pageindex";
});


builder.Services.AddMemoryCache(); // Adiciona o serviço de cache em memória
builder.Services.AddSession(); // Adiciona o suporte para sessões

var app = builder.Build(); // Constrói a aplicação

// Configurar o pipeline de requisição HTTP
if (!app.Environment.IsDevelopment()) // Se não estiver em ambiente de desenvolvimento
{
    app.UseExceptionHandler("/Home/Error"); // Usa o manipulador de exceções para redirecionar para a página de erro
    app.UseHsts(); // Usa HTTP Strict Transport Security (HSTS) para reforçar a segurança
}
app.UseHttpsRedirection(); // Redireciona as requisições HTTP para HTTPS
app.UseStaticFiles(); // Serve arquivos estáticos
app.UseSession(); // Habilita o uso de sessões
app.UseRouting(); // Habilita o roteamento

app.UseAuthentication(); // Habilita a autenticação
app.UseAuthorization(); // Habilita a autorização

// Configuração das rotas
app.MapControllerRoute(
    name: "list",  // Define o nome da rota como "list", o que pode ser útil para gerar URLs ou fazer referência a essa rota em outras partes da aplicação
    pattern: "Lanche/{action=List}/{categoria?}", // Define o padrão da URL que será mapeado para esta rota
    defaults: new { controller = "Lanche", action = "List" } // Define valores padrão para os parâmetros da rota
);

app.MapControllerRoute( // Rota área admin
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default", // Define a rota padrão
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Padrão de rota que mapeia para controladores e ações

// Inicializa as roles e os users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seedUserRolesInitial = services.GetRequiredService<ISeedUserRolesInitial>();
    seedUserRolesInitial.SeedRoles();
    seedUserRolesInitial.SeedUsers();
}

app.Run(); // Executa a aplicação
