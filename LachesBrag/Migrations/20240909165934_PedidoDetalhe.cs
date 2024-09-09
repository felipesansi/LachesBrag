using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LachesBrag.Migrations
{
    public partial class PedidoDetalhe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(999)", maxLength: 999, nullable: false),
                    SobreNome = table.Column<string>(type: "nvarchar(999)", maxLength: 999, nullable: false),
                    Endereco1 = table.Column<string>(type: "nvarchar(999)", maxLength: 999, nullable: false),
                    Endereco2 = table.Column<string>(type: "nvarchar(999)", maxLength: 999, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(999)", maxLength: 999, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(999)", maxLength: 999, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PedidoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalItensPedidos = table.Column<int>(type: "int", nullable: false),
                    PedidoEnviado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PedididoEntregue = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.PedidoId);
                });

            migrationBuilder.CreateTable(
                name: "PedidosDetalhe",
                columns: table => new
                {
                    PedidoDetalheId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    LancheId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosDetalhe", x => x.PedidoDetalheId);
                    table.ForeignKey(
                        name: "FK_PedidosDetalhe_Lanches_LancheId",
                        column: x => x.LancheId,
                        principalTable: "Lanches",
                        principalColumn: "LancheId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosDetalhe_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalhe_LancheId",
                table: "PedidosDetalhe",
                column: "LancheId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalhe_PedidoId",
                table: "PedidosDetalhe",
                column: "PedidoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosDetalhe");

            migrationBuilder.DropTable(
                name: "Pedido");
        }
    }
}
