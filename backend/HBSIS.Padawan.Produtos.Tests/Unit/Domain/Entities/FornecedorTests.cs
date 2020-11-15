using FluentAssertions;
using HBSIS.Padawan.Produtos.Domain.builder;
using HBSIS.Padawan.Produtos.Tests.Builders;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Domain.Entities
{
    public class FornecedorTests
    {
        [Fact]
        public void Possuir_Dados_Fornecedor()
        {
            var IdFornecedor = 10;
            var fornecedor = new FornecedorBuilderTest()
                                  .ComCNPJ("08.632.688/0001-34")
                                  .ComEmail("barbara@gmail.com")
                                  .ComRua("Rua Paraguay")
                                  .ComBairro("Ponta Aguda")
                                  .ComCidade("Blumenau")
                                  .ComNumero("270")
                                  .ComUF("SC")
                                  .ComComplemento("502")
                                  .ComCEP("89050020")
                                  .ComId(IdFornecedor)
                                  .ComNomeFantasia("Barbaras Comesticos e Batom")
                                  .ComRazaoSocial("Barbara Comesticos")
                                  .ComTelefone("47991085945")
                                  .Build();

            fornecedor.CNPJ.Should().Be("08.632.688/0001-34");
            fornecedor.Email.Should().Be("barbara@gmail.com");
            fornecedor.Telefone.Should().Be("47991085945");
            fornecedor.Endereco.Bairro.Should().Be("Ponta Aguda");
            fornecedor.Endereco.Rua.Should().Be("Rua Paraguay");
            fornecedor.Endereco.Cidade.Should().Be("Blumenau");
            fornecedor.Endereco.Numero.Should().Be("270");
            fornecedor.Endereco.UF.Should().Be("SC");
            fornecedor.Endereco.Complemento.Should().Be("502");
            fornecedor.Endereco.CEP.Should().Be("89050020");
            fornecedor.NomeFantasia.Should().Be("Barbaras Comesticos e Batom");
            fornecedor.Id.Should().Be(IdFornecedor);
            fornecedor.RazaoSocial.Should().Be("Barbara Comesticos");
        }

        [Fact]
        public void Deletar_Fornecedor()
        {
            var fornecedorId = 1;
            var fornecedor = new FornecedorBuilderTest()
                .ComRazaoSocial("barbara Cosmeticos")
                .ComCNPJ("97.598.988/0001-74")
                .ComNomeFantasia("Claudio")
                .ComBairro("Ponta Aguda")
                .ComCidade("Blumenau")
                .ComRua("rua Paraguay")
                .ComNumero("270")
                .ComComplemento("Apto 502")
                .ComUF("SC")
                .ComCEP("89050020")
                .ComEmail("babitief@gmail.com")
                .ComTelefone("433222335")
                .ComId(fornecedorId)
                .Build();

            fornecedor.Delete();
            fornecedor.Deletado.Should().BeTrue();
        }

        [Fact]
        public void Atualizar_Fornecedor()
        {
            var fornecedorId = 1;
            var fornecedor = new FornecedorBuilder()
                .ComRazaoSocial("barbara Cosmeticos")
                .ComCNPJ("97.598.988/0001-74")
                .ComNomeFantasia("Claudio")
                .ComBairro("Ponta Aguda")
                .ComCidade("Blumenau")
                .ComRua("Rua Paraguay")
                .ComNumero("270")
                .ComComplemento("Apto 502")
                .ComUF("SC")
                .ComCEP("89050020")
                .ComEmail("babitief@gmail.com")
                .ComTelefone("433222335")
                .ComId(fornecedorId)
                .Build();

            fornecedor.Update(new FornecedorBuilder()
                .ComRazaoSocial("Kruger Cosméticos")
                .ComCNPJ("08.632.688/0001-34")
                .ComNomeFantasia("Kruge Beleza")
                .ComBairro("Escola Agricola")
                .ComCidade("Blumenau")
                .ComRua("Rua Joinville")
                .ComNumero("77")
                .ComComplemento("Sala 502")
                .ComUF("SC")
                .ComCEP("89037656")
                .ComEmail("krugerCosmeticos@gmail.com")
                .ComTelefone("4733990785")
                .ComId(fornecedorId)
                );

            fornecedor.CNPJ.Should().Be("08.632.688/0001-34");
            fornecedor.Email.Should().Be("krugerCosmeticos@gmail.com");
            fornecedor.Telefone.Should().Be("4733990785");
            fornecedor.Endereco.Bairro.Should().Be("Escola Agricola");
            fornecedor.Endereco.Rua.Should().Be("Rua Joinville");
            fornecedor.Endereco.Cidade.Should().Be("Blumenau");
            fornecedor.Endereco.Numero.Should().Be("77");
            fornecedor.Endereco.UF.Should().Be("SC");
            fornecedor.Endereco.Complemento.Should().Be("Sala 502");
            fornecedor.Endereco.CEP.Should().Be("89037656");
            fornecedor.NomeFantasia.Should().Be("Kruge Beleza");
            fornecedor.Id.Should().Be(fornecedorId);
            fornecedor.RazaoSocial.Should().Be("Kruger Cosméticos");

        }
    }
}

