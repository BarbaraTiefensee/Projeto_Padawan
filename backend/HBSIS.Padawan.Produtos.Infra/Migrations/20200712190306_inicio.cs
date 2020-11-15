using Microsoft.EntityFrameworkCore.Migrations;

namespace HBSIS.Padawan.Produtos.Infra.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FORNECEDORES",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deletado = table.Column<bool>(nullable: false),
                    RazaoSocial = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    CNPJ = table.Column<string>(unicode: false, fixedLength: true, maxLength: 18, nullable: false),
                    NomeFantasia = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Fornecedor_Rua = table.Column<string>(type: "varchar(80)", unicode: false, nullable: true),
                    Fornecedor_Bairro = table.Column<string>(type: "varchar(50)", unicode: false, nullable: true),
                    Fornecedor_Cidade = table.Column<string>(type: "varchar(40)", unicode: false, nullable: true),
                    Fornecedor_CEP = table.Column<string>(type: "varchar(8)", unicode: false, nullable: true),
                    Fornecedor_Numero = table.Column<string>(type: "varchar(6)", unicode: false, nullable: true),
                    Fornecedor_Complemento = table.Column<string>(type: "varchar(70)", unicode: false, nullable: true),
                    Fornecedor_UF = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
                    Telefone = table.Column<string>(unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORNECEDORES", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FORNECEDORES");
        }
    }
}
