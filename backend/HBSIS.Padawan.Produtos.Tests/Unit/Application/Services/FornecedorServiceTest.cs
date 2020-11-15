using FluentAssertions;
using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Models;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Tests.Builders;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Application.Services
{
    public class FornecedorServiceTest
    {
        private readonly IFornecedorServices _fornecedorServices;
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorServiceTest()
        {
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();
            _fornecedorServices = new FornecedorServices(_fornecedorRepository);
        }

        [Fact]
        public async Task Salvar_Fornecedor_Create()
        {
           
            FornecedorRequestModel fornecedorRequestModel = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Carmello Bogo",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },
                RazaoSocial = "Barbara Cosmeticos",
                CNPJ = "89.096.092/0001-69",
                NomeFantasia = "Barbara Comesticos LTDa",
                Telefone = "47991085345",
                Email = "babitief@gmail.com",
            };
        
            await _fornecedorServices.Create(fornecedorRequestModel);
            await _fornecedorRepository.Received(1).Create(Arg.Any<FornecedorEntity>());
        }
        [Fact]
        public async Task Estourar_Excecao_FornecedorJaExiste_Create()
        {
            var fornecedorRequestModel = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Carmello Bogo",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },
                RazaoSocial = "Casas Bahia",
                CNPJ = "89.096.092/0001-69",
                NomeFantasia = "Casas Bahia a casa da familia",
                Telefone = "47991085345",
                Email = "babitief@gmail.com",
            };

            _fornecedorRepository.VerificarSeFornecedorJaExiste(fornecedorRequestModel.CNPJ).Returns(true);

            var ex = await Record.ExceptionAsync(() => _fornecedorServices.Create(fornecedorRequestModel));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Fornecedor já existe.");
        }

        [Fact]
        public async Task Pegar_Fornecedor_GetByID()
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

            _fornecedorRepository.GetById(fornecedorId).Returns(fornecedor);
            var fornecedorRetornado = await _fornecedorServices.GetById(fornecedorId);
            fornecedorRetornado.Should().NotBeNull();

            fornecedorRetornado.RazaoSocial.Should().Be(fornecedor.RazaoSocial);
            fornecedorRetornado.CNPJ.Should().Be(fornecedor.CNPJ);
            fornecedorRetornado.NomeFantasia.Should().Be(fornecedor.NomeFantasia);
            fornecedorRetornado.Endereco.Rua.Should().Be(fornecedor.Endereco.Rua);
            fornecedorRetornado.Endereco.Bairro.Should().Be(fornecedor.Endereco.Bairro);
            fornecedorRetornado.Endereco.Cidade.Should().Be(fornecedor.Endereco.Cidade);
            fornecedorRetornado.Endereco.Numero.Should().Be(fornecedor.Endereco.Numero);
            fornecedorRetornado.Endereco.Complemento.Should().Be(fornecedor.Endereco.Complemento);
            fornecedorRetornado.Endereco.UF.Should().Be(fornecedor.Endereco.UF);
            fornecedorRetornado.Endereco.CEP.Should().Be(fornecedor.Endereco.CEP);
            fornecedorRetornado.Telefone.Should().Be(fornecedor.Telefone);
            fornecedorRetornado.Email.Should().Be(fornecedor.Email);
            fornecedorRetornado.Id.Should().Be(fornecedorId);
        }
        [Fact]
        public async Task Estourar_Excecao_GetByID()
        {
            var fornecedorID = 1;

            var fornecedorRequestModel = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Carmello Bogo",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },
                RazaoSocial = "Casas Bahia",
                CNPJ = "89.096.092/0001-69",
                NomeFantasia = "Casas Bahia a casa da familia",
                Telefone = "47991085345",
                Email = "babitief@gmail.com",
            };

            var ex = await Record.ExceptionAsync(() => _fornecedorServices.GetById(fornecedorID));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Id do fornecedor inexistente.");
        }

        [Fact]
        public async Task Deletar_Fornecedor_Delete()
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
                .ComEmail("mabelksouza@gmail.com")
                .ComComplemento("Apto 502")
                .ComCEP("89037656")
                .ComUF("SC")
                .ComTelefone("433222335")
                .ComId(fornecedorId)
                .Build();

            _fornecedorRepository.GetById(fornecedorId).Returns(fornecedor);
            await _fornecedorServices.Delete(fornecedorId);
            await _fornecedorRepository.Received(1).Delete(Arg.Any<FornecedorEntity>());
        }
        [Fact]
        public async Task Estourar_Excecao_Delete()
        {
            var fornecedorID = 1;
            var fornecedorRequestModel = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Carmello Bogo",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },
                RazaoSocial = "Casas Bahia",
                CNPJ = "89.096.092/0001-69",
                NomeFantasia = "Casas Bahia a casa da familia",
                Telefone = "47991085345",
                Email = "babitief@gmail.com",
            };

            var ex = await Record.ExceptionAsync(() => _fornecedorServices.Delete(fornecedorID));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Id inexistente.");
        }

        [Fact]
        public async Task Atualizar_Fornecedor_Update()
        {
            var fornecedorId = 1;

            var model = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Carmello Bogo",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },
                RazaoSocial = "Casas Bahia",
                CNPJ = "89.096.092/0001-69",
                NomeFantasia = "Casas Bahia a casa da familia",
                Telefone = "47991085345",
                Email = "babitief@gmail.com",
            };

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

            _fornecedorRepository.GetById(fornecedorId).Returns(fornecedor);
            _fornecedorRepository.ExisteFornecedorComEsseCnpj(fornecedor.CNPJ, fornecedorId).Returns(false);
            var fornecedorRetornado = await _fornecedorServices.Update(fornecedorId, model);
          
            await _fornecedorRepository.Received(1).Update(Arg.Is<FornecedorEntity>(args => 
                args.RazaoSocial == model.RazaoSocial
                && args.CNPJ == model.CNPJ
                && args.NomeFantasia == model.NomeFantasia
                && args.Endereco.Rua == model.Endereco.Rua
                && args.Endereco.Bairro == model.Endereco.Bairro
                && args.Endereco.Cidade == model.Endereco.Cidade
                && args.Endereco.Numero == model.Endereco.Numero
                && args.Endereco.Complemento == model.Endereco.Complemento
                && args.Endereco.UF == model.Endereco.UF
                && args.Endereco.CEP == model.Endereco.CEP
                && args.Telefone == model.Telefone
                && args.Email == model.Email));
        }
        [Fact]
        public async Task Estourar_Excecao_CnpjJaExiste_Update() 
        {
            var fornecedorId = 1;
            var fornecedorRequestModel = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Carmello Bogo",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },
                
                RazaoSocial = "Casas Bahia",
                CNPJ = "89.096.092/0001-69",
                NomeFantasia = "Casas Bahia a casa da familia",
                Telefone = "47991085345",
                Email = "babitief@gmail.com",
            };

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

            _fornecedorRepository.GetById(fornecedorId).Returns(fornecedor);
            _fornecedorRepository.ExisteFornecedorComEsseCnpj(fornecedorRequestModel.CNPJ, fornecedorId).Returns(true);

            var ex = await Record.ExceptionAsync(() => _fornecedorServices.Update(fornecedorId, fornecedorRequestModel));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Fornecedor não poderá ser cadastrado.");
        }
        [Fact]
        public async Task Estourar_Excexao_IdNulo_Update()
        {
            var fornecedorId = 1;
            var fornecedorRequestModel = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Carmello Bogo",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = "Casas Bahia",
                CNPJ = "89.096.092/0001-69",
                NomeFantasia = "Casas Bahia a casa da familia",
                Telefone = "47991085345",
                Email = "babitief@gmail.com",
            };

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
                .Build();

            var ex = await Record.ExceptionAsync(() => _fornecedorServices.Update(fornecedorId, fornecedorRequestModel));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Id invalido.");
        }
    }
}
