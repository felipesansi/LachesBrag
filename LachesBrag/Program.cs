using Microsoft.EntityFrameworkCore;
using LachesBrag.Context;
using LachesBrag.Repositories.Interfaces;
using LanchesBrag.Models;
using Microsoft.AspNetCore.Identity;

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


// configura��o de requisitos para senhas de cadastro no sistema
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});


// Adicionar servi�os ao cont�iner de inje��o de depend�ncia.
builder.Services.AddControllersWithViews(); // Adiciona o suporte para controladores e views MVC

// Registro dos reposit�rios
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>(); // Registra o reposit�rio de categoria como um servi�o transit�rio
builder.Services.AddTransient<ILanchesRepository, LanchesRepository>(); // Registra o reposit�rio de lanches como um servi�o transit�rio
builder.Services.AddTransient<IPeddidoRepository, PedidoRepository>(); // Registra o reposit�rio de pedido como um servi�o transit�rio


// Registra o servi�o `CarrinhoCompra` no cont�iner de inje��o de depend�ncias com o ciclo de vida "Scoped".
// Isso significa que uma inst�ncia �nica do `CarrinhoCompra` ser� criada por requisi��o.
// O m�todo `GetCarrinho` � chamado para obter uma inst�ncia do carrinho, que ser� injetada onde necess�rio.
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Adiciona o HttpContextAccessor como um servi�o singleton

builder.Services.AddMemoryCache(); // Adiciona o servi�o de cache em mem�ria
builder.Services.AddSession(); // Adiciona o suporte para sess�es

var app = builder.Build(); // Constr�i a aplica��o

// Configurar o pipeline de requisi��o HTTP.
if (!app.Environment.IsDevelopment()) // Se n�o estiver em ambiente de desenvolvimento
{
    app.UseExceptionHandler("/Home/Error"); // Usa o manipulador de exce��es para redirecionar para a p�gina de erro
    app.UseHsts(); // Usa HTTP Strict Transport Security (HSTS) para refor�ar a seguran�a
}
app.UseAuthentication(); // / Habilita a autentica��o
app.UseHttpsRedirection(); // Redireciona as requisi��es HTTP para HTTPS
app.UseStaticFiles(); // Serve arquivos est�ticos
app.UseSession(); // Habilita o uso de sess�es
app.UseRouting(); // Habilita o roteamento

app.UseAuthorization(); // Habilita a autoriza��o


// Configura��o das rotas
app.MapControllerRoute(
    name: "list",  // Define o nome da rota como "list", o que pode ser �til para gerar URLs ou fazer refer�ncia a essa rota em outras partes da aplica��o.
    pattern: "Lanche/{action=List}/{categoria?}", // Define o padr�o da URL que ser� mapeado para esta rota. Neste caso:
                                                  // - "Lanche" � o segmento fixo da URL.
                                                  // - "{action=List}" define um par�metro de URL opcional chamado "action" que, se n�o fornecido, ser� "List" por padr�o.
                                                  // - "{categoria?}" � um par�metro opcional de URL chamado "categoria". O "?" indica que esse par�metro � opcional.
    defaults: new { controller = "Lanche", action = "List" } // Define valores padr�o para os par�metros da rota. 
                                                             // Se o "controller" n�o for especificado na URL, ele ser� definido como "Lanche".
                                                             // Se o "action" n�o for especificado, ser� definido como "List".
);


app.MapControllerRoute(
    name: "default", // Define a rota padr�o
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Padr�o de rota que mapeia para controladores e a��es

app.Run(); // Executa a aplica��o
