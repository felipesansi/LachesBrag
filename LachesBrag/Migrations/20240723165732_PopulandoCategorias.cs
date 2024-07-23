using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LachesBrag.Migrations
{
    public partial class PopulandoCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categorias (CategoriaNome, Descricao) values ('Normal', 'Lanches com ingredientes nomais')");

            migrationBuilder.Sql("insert into Categorias (CategoriaNome, Descricao) values ('Naturais', 'Lanches com ingredientes naturais')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
