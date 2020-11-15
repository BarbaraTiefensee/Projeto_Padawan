using FluentAssertions;
using HBSIS.Padawan.Produtos.Application.Models.CategoriaModel;
using HBSIS.Padawan.Produtos.Application.Services.CategoriaServices;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Tests.Builders;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Application.Services
{
    public class CategoriaServiceTest
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaServices _categoriaServices;
        private readonly IFormFile _iIformFile;

        public CategoriaServiceTest()
        {
            _categoriaRepository = Substitute.For<ICategoriaRepository>();
            _categoriaServices = new CategoriaServices(_categoriaRepository);
            _iIformFile = Substitute.For<IFormFile>();
        }

        [Fact]
        public async Task Salvar_Create()
        {
            CategoriaRequestModel categoriaRequest = new CategoriaRequestModel()
            {
                FornecedorId = 1,
                NomeCategoria = "Bebidas",
            };

            await _categoriaServices.Create(categoriaRequest);
            await _categoriaRepository.Received(1).Create(Arg.Any<CategoriaEntity>());
        }

        [Fact]
        public async Task Estourar_Execao_VerificarSeJaExisteCategoria_Create()
        {
            var idFornecedor = 4;
            CategoriaRequestModel categoriaRequest = new CategoriaRequestModel()
            {
                NomeCategoria = "Bebidas",
                FornecedorId = idFornecedor
            };

            _categoriaRepository.VerificarSeJaExisteCategoria(categoriaRequest.NomeCategoria).Returns(true);
            var ex = await Record.ExceptionAsync(() => _categoriaServices.Create(categoriaRequest));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Categoria já existe.");

        }

        [Fact]
        public async Task Pegar_GetAll()
        {
            var idFornecedor1 = 2;
            var idCategoria1 = 1;
            var categoria1 = new CategoriaBuilderTest()
                .ComNomeCategoria("Água Mineral")
                .ComIdFornecedor(idFornecedor1)
                .ComId(idCategoria1)
                .Construir();

            var idFornecedor2 = 4;
            var idCategoria2 = 2;
            var categoria2 = new CategoriaBuilderTest()
                .ComNomeCategoria("Refrigerantes")
                .ComIdFornecedor(idFornecedor2)
                .ComId(idCategoria2)
                .Construir();

            var categorias = new List<CategoriaEntity>
            {
                categoria1,
                categoria2
            };

            _categoriaRepository.GetAll().Returns(categorias);
            var categoriasRetornados = await _categoriaServices.GetAll();
            categoriasRetornados.Should().HaveCount(2);

            categoriasRetornados.Any(c => c.NomeCategoria == categoria1.NomeCategoria
                                          && c.FornecedorId == categoria1.FornecedorId
                                          && c.Deletado == categoria1.Deletado
                                          && c.Id == categoria1.Id).Should().BeTrue();
            categoriasRetornados.Any(c => c.NomeCategoria == categoria2.NomeCategoria
                                         && c.FornecedorId == categoria2.FornecedorId
                                         && c.Deletado == categoria2.Deletado
                                         && c.Id == categoria2.Id).Should().BeTrue();
        }

        [Fact]
        public async Task Pegar_GetById()
        {
            var idFornecedor = 4;
            var idCategoria = 1;
            var categoria = new CategoriaBuilderTest()
                .ComNomeCategoria("Água Mineral")
                .ComIdFornecedor(idFornecedor)
                .ComId(idCategoria)
                .Construir();

            _categoriaRepository.GetById(idCategoria).Returns(categoria);
            var categoriaRetornado = await _categoriaServices.GetById(idCategoria);
            categoriaRetornado.Should().NotBeNull();
        }

        [Fact]
        public async Task Estourar_Execao_GetById()
        {
            var idCategoria = 1;
            var categoriaRequest = new CategoriaRequestModel()
            {
                NomeCategoria = "Agua"
            };

            var ex = await Record.ExceptionAsync(() => _categoriaServices.GetById(idCategoria));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Id da Categoria inexistente.");
        }

        [Fact]
        public async Task Estourar_Excecao_Deletado_GetById()
        {
            var idCategoria = 1;
            var idFornecedor = 4;
            var categoria = new CategoriaBuilderTest()
                .ComNomeCategoria("Refrigerante")
                .ComIdFornecedor(idFornecedor)
                .ComId(idCategoria)
                .Deletar()
                .Construir();

            _categoriaRepository.GetById(idCategoria).Returns(categoria);
        }

        [Fact]
        public async Task Categoria_Deletar()
        {
            var idFornecedor = 4;
            var idCategoria = 1;
            var categoria = new CategoriaBuilderTest()
                .ComNomeCategoria("Agua")
                .ComIdFornecedor(idFornecedor)
                .ComId(idCategoria)
                .Construir();

            _categoriaRepository.GetById(idCategoria).Returns(categoria);
            await _categoriaServices.Delete(idCategoria);
            await _categoriaRepository.Received(1).Delete(Arg.Any<CategoriaEntity>());
        }

        [Fact]
        public async Task Estourar_Excecao_Deletar()
        {
            var idCategoria = 1;
            var categoriaRequest = new CategoriaRequestModel
            {
                NomeCategoria = "Refrigerante"
            };

            var ex = await Record.ExceptionAsync(() => _categoriaServices.Delete(idCategoria));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Id inexistente.");
        }

        [Fact]
        public async Task Atualizar_Update()
        {
            var idCategoria = 1;
            var idFornecedor = 2;
            CategoriaRequestModel categoriaRequest = new CategoriaRequestModel()
            {
                NomeCategoria = "Bebidas",
                FornecedorId = idFornecedor,
                Id = idCategoria
            };

            var categoriaBuilder = new CategoriaBuilderTest()
                .ComNomeCategoria("Comida")
                .ComIdFornecedor(idFornecedor)
                .ComId(idCategoria)
                .Construir();

            _categoriaRepository.GetById(idFornecedor).Returns(categoriaBuilder);
            _categoriaRepository.VerificarSeExisteCategoriaComEsseNome(categoriaBuilder.NomeCategoria, categoriaBuilder.FornecedorId).Returns(false);
            var categoriaRetornado = await _categoriaServices.Update(idFornecedor, categoriaRequest);

            await _categoriaRepository.Received(1).Update(Arg.Is<CategoriaEntity>(args =>
            args.NomeCategoria == categoriaRequest.NomeCategoria
            && args.FornecedorId == categoriaRequest.FornecedorId
            && args.Id == categoriaRequest.Id));
        }

        [Fact]
        public async Task Estourar_Excecao_VerificarSeExisteCategoriaComEsseNome_Update()
        {
            var idCategoria = 3;
            var idFornecedor = 7;
            var categoriaRequestModel = new CategoriaRequestModel()
            {
                NomeCategoria = "Bebidas",
                FornecedorId = idFornecedor,
                Id = idCategoria
            };

            var categoria = new CategoriaBuilderTest()
                .ComNomeCategoria("Agua")
                .ComIdFornecedor(idFornecedor)
                .ComId(idCategoria)
                .Construir();

            _categoriaRepository.GetById(idCategoria).Returns(categoria);
            _categoriaRepository.VerificarSeExisteCategoriaComEsseNome(categoriaRequestModel.NomeCategoria, idCategoria).Returns(true);
            var ex = await Record.ExceptionAsync(() => _categoriaServices.Update(idCategoria, categoriaRequestModel));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Nome da Categoria já existente.");
        }

        [Fact]
        public async Task Estourar_Excecao_IdNullo_Update()
        {
            var idFornecedor = 4;
            var idCategoria = 1;
            var categoriaRequestModel = new CategoriaRequestModel()
            {
                NomeCategoria = "Casas Bahia",
                FornecedorId = idFornecedor,
                Id = idCategoria
            };

            var categoria = new CategoriaBuilderTest()
                .ComNomeCategoria("Bebidas")
                .ComIdFornecedor(idFornecedor)
                .ComId(idCategoria)
                .Construir();

            var ex = await Record.ExceptionAsync(() => _categoriaServices.Update(idCategoria, categoriaRequestModel));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Id da categoria inválida.");
        }

        [Fact]
        public async Task Exportar_CSV()
        {
            var idFornecedor1 = 2;
            var categoria1 = new CategoriaBuilderTest()
                .ComNomeCategoria("Água Mineral")
                .ComIdFornecedor(idFornecedor1)
                .ComFornecedor("Disney")
                .Export();

            var idFornecedor2 = 4;
            var categoria2 = new CategoriaBuilderTest()
                .ComNomeCategoria("Refrigerantes")
                .ComIdFornecedor(idFornecedor2)
                .ComFornecedor("Ambev")
                .Export();

            var categorias = new List<CategoriaEntity>
            {
                categoria1,
                categoria2
            };

            _categoriaRepository.GetAllExportCsv().Returns(categorias);

            var categoriaCsv = await _categoriaServices.ExportCategoria();
            categoriaCsv.Should().Contain("Água Mineral", "Refrigerantes");
            categoriaCsv.Should().NotBeNull();
        }

        [Fact]
        public async Task Importar_Excel()
        {
            var dadosDasCategoriasEmCSV = "CABECALHO;CABECALHO \nAgua Mineral;2 \nRefrigerante;1";
            var dadosDoArquivo = new MemoryStream(Encoding.UTF8.GetBytes(dadosDasCategoriasEmCSV));

            _iIformFile.OpenReadStream().Returns(dadosDoArquivo);
            var categoriaImport = await _categoriaServices.ImportCategoria(_iIformFile);

            categoriaImport.Should().HaveCount(2);

            categoriaImport.Any(c => c.NomeCategoria == "Agua Mineral" && c.FornecedorId == 2).Should().BeTrue();
            categoriaImport.Any(c => c.NomeCategoria == "Refrigerante" && c.FornecedorId == 1).Should().BeTrue();
        }

        [Fact]
        public async Task Estourar_Excecao_Import_Excel()
        {
            var dadosDasCategoriasEmCSV = "CABECALHO;CABECALHO \nAgua Mineral;2 \nRefrigerante;1";
            var dadosDoArquivo = new MemoryStream(Encoding.UTF8.GetBytes(dadosDasCategoriasEmCSV));

            _iIformFile.OpenReadStream().Returns(dadosDoArquivo);
            _categoriaRepository.VerificarSeJaExisteCategoria("Refrigerante").Returns(true);

            var ex = await Record.ExceptionAsync(() => _categoriaServices.ImportCategoria(_iIformFile));
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public async Task Excecao_FornecedorNulo_Importar_Excel()
        {
            var dadosDasCategoriasEmCSV = "CABECALHO;CABECALHO \nAgua Mineral \nRefrigerante;1";
            var dadosDoArquivo = new MemoryStream(Encoding.UTF8.GetBytes(dadosDasCategoriasEmCSV));

            _iIformFile.OpenReadStream().Returns(dadosDoArquivo);

            var ex = await Record.ExceptionAsync(() => _categoriaServices.ImportCategoria(_iIformFile));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Dados do CSV inválidos.");
        }

        [Fact]
        public async Task Excecao_CategoriaNula_Importar_Excel()
        {
            var dadosDasCategoriasEmCSV = "CABECALHO;CABECALHO \n2 \nRefrigerante;1";
            var dadosDoArquivo = new MemoryStream(Encoding.UTF8.GetBytes(dadosDasCategoriasEmCSV));

            _iIformFile.OpenReadStream().Returns(dadosDoArquivo);

            var ex = await Record.ExceptionAsync(() => _categoriaServices.ImportCategoria(_iIformFile));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Dados do CSV inválidos.");
        }

        [Fact]
        public async Task Excecao_CategoriaFornecedorNulo_Importar_Excel()
        {
            var dadosDasCategoriasEmCSV = "CABECALHO;CABECALHO";
            var dadosDoArquivo = new MemoryStream(Encoding.UTF8.GetBytes(dadosDasCategoriasEmCSV));

            _iIformFile.OpenReadStream().Returns(dadosDoArquivo);

            var ex = await Record.ExceptionAsync(() => _categoriaServices.ImportCategoria(_iIformFile));
            ex.Should().BeOfType<ArgumentException>();
            ex.Message.Should().Be("Dados do CSV não encontrados.");
        }
    }
}