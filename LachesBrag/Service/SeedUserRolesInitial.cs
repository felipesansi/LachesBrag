using Microsoft.AspNetCore.Identity; 
using System.Data;

namespace LachesBrag.Service
{
    public class SeedUserRolesInitial : ISeedUserRolesInitial // Define a classe que implementa a interface ISeedUserRolesInitial.
    {
        private readonly UserManager<IdentityUser> _UserManager; // Declara uma variável para gerenciar usuários.
        private readonly RoleManager<IdentityRole> _RoleManager; // Declara uma variável para gerenciar funções (roles).

        // Construtor da classe, inicializa as variáveis UserManager e RoleManager através de injeção de dependência.
        public SeedUserRolesInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _UserManager = userManager;
            _RoleManager = roleManager;
        }

        // Método para semear (criar) funções (roles) iniciais.
        public void SeedRoles()
        {
            // Verifica se a função "Member" não existe.
            if (!_RoleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole(); // Cria uma nova instância de IdentityRole.
                role.Name = "Member"; // Define o nome da função.
                role.NormalizedName = "MEMBER"; // Define o nome normalizado da função.
                IdentityResult roleResult = _RoleManager.CreateAsync(role).Result; // Cria a função no banco de dados.
            }

            // Verifica se a função "Admin" não existe.
            if (!_RoleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role2 = new IdentityRole(); // Cria uma nova instância de IdentityRole.
                role2.Name = "Admin"; // Define o nome da função.
                role2.NormalizedName = "ADMIN"; // Define o nome normalizado da função.
                IdentityResult roleResult = _RoleManager.CreateAsync(role2).Result; // Cria a função no banco de dados.
            }
        }

        // Método para semear (criar) usuários iniciais.
        public void SeedUsers()
        {
            // Verifica se o usuário com o email "usuario@localhost" não existe.
            if (_UserManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser(); // Cria uma nova instância de IdentityUser.
                user.UserName = "usuario@localhost"; // Define o nome de usuário.
                user.NormalizedUserName = "USUARIO@LOCALHOST"; // Define o nome de usuário normalizado.
                user.NormalizedEmail = "USUARIO@LOCALHOST"; // Define o email normalizado.
                user.EmailConfirmed = true; // Confirma o email do usuário.
                user.LockoutEnabled = false; // Desabilita o bloqueio de conta.
                user.SecurityStamp = Guid.NewGuid().ToString(); // Gera um novo carimbo de segurança.

                // Cria o usuário no banco de dados com a senha especificada.
                IdentityResult Resut = _UserManager.CreateAsync(user, "Nusey#2024").Result;
                if (Resut.Succeeded) // Verifica se a criação do usuário foi bem-sucedida.
                {
                    _UserManager.AddToRoleAsync(user, "Member").Wait(); // Adiciona o usuário à função "Member".
                }
            }

            // Verifica se o usuário com o email "admin@localhost" não existe.
            if (_UserManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser(); // Cria uma nova instância de IdentityUser.
                user.UserName = "admin@localhost"; // Define o nome de usuário.
                user.NormalizedUserName = "ADMIN@LOCALHOST"; // Define o nome de usuário normalizado.
                user.NormalizedEmail = "ADMIN@LOCALHOST"; // Define o email normalizado.
                user.EmailConfirmed = true; // Confirma o email do usuário.
                user.LockoutEnabled = false; // Desabilita o bloqueio de conta.
                user.SecurityStamp = Guid.NewGuid().ToString(); // Gera um novo carimbo de segurança.

                // Cria o usuário no banco de dados com a senha especificada.
                IdentityResult Resut = _UserManager.CreateAsync(user, "Nusey#2024").Result;
                if (Resut.Succeeded) // Verifica se a criação do usuário foi bem-sucedida.
                {
                    _UserManager.AddToRoleAsync(user, "Admin").Wait(); // Adiciona o usuário à função "Admin".
                }
            }
        }
    }
}
