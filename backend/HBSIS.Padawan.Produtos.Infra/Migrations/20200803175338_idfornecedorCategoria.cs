using Microsoft.EntityFrameworkCore.Migrations;

namespace HBSIS.Padawan.Produtos.Infra.Migrations
{
    public partial class idfornecedorCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CATEGORIAS_FORNECEDORES_Fornecedores",
                table: "CATEGORIAS");

            migrationBuilder.RenameColumn(
                name: "Fornecedores",
                table: "CATEGORIAS",
                newName: "FornecedorID");

            migrationBuilder.RenameIndex(
                name: "IX_CATEGORIAS_Fornecedores",
                table: "CATEGORIAS",
                newName: "IX_CATEGORIAS_FornecedorID");

            migrationBuilder.AddForeignKey(
                name: "FK_CATEGORIAS_FORNECEDORES_FornecedorID",
                table: "CATEGORIAS",
                column: "FornecedorID",
                principalTable: "FORNECEDORES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CATEGORIAS_FORNECEDORES_FornecedorID",
                table: "CATEGORIAS");

            migrationBuilder.RenameColumn(
                name: "FornecedorID",
                table: "CATEGORIAS",
                newName: "Fornecedores");

            migrationBuilder.RenameIndex(
                name: "IX_CATEGORIAS_FornecedorID",
                table: "CATEGORIAS",
                newName: "IX_CATEGORIAS_Fornecedores");

            migrationBuilder.AddForeignKey(
                name: "FK_CATEGORIAS_FORNECEDORES_Fornecedores",
                table: "CATEGORIAS",
                column: "Fornecedores",
                principalTable: "FORNECEDORES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
