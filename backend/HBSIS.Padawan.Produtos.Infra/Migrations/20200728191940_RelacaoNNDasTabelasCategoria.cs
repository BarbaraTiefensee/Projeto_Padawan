using Microsoft.EntityFrameworkCore.Migrations;

namespace HBSIS.Padawan.Produtos.Infra.Migrations
{
    public partial class RelacaoNNDasTabelasCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIAS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deletado = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Fornecedores = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CATEGORIAS_FORNECEDORES_Fornecedores",
                        column: x => x.Fornecedores,
                        principalTable: "FORNECEDORES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIAS_Fornecedores",
                table: "CATEGORIAS",
                column: "Fornecedores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CATEGORIAS");
        }
    }
}
