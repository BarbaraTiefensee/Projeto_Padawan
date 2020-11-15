
using FluentAssertions;
using HBSIS.Padawan.Produtos.Application.Services.CategoriaServices;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Web.Controller
{
    public class CategoriaControllerTest
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaServices _iCategoriaServices;
        private readonly CategoriaController _categoriaController;
        private readonly IFormFile _iIformFile;

        public CategoriaControllerTest()
        {
            _categoriaRepository = Substitute.For<ICategoriaRepository>();
            _iCategoriaServices = Substitute.For<ICategoriaServices>();
            _categoriaController = new CategoriaController(_iCategoriaServices);
            _iIformFile = Substitute.For<IFormFile>();
        }

        [Fact]
        public async Task Importar_Categoria()
        {
            var dadosDasCategoriasEmCSV = "CABECALHO;CABECALHO \nAgua Mineral;2 \nRefrigerante;1";
            var dadosDoArquivo = new MemoryStream(Encoding.UTF8.GetBytes(dadosDasCategoriasEmCSV));

            _iIformFile.OpenReadStream().Returns(dadosDoArquivo);

            var controller = await _categoriaController.Import(_iIformFile);

            controller.Should().NotBeNull();
            controller.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Excecao_Importar_Categoria()
        {
            var dadosDasCategoriasEmCSV = "CABECALHO;CABECALHO \nAgua Mineral \nRefrigerante;1";
            var dadosDoArquivo = new MemoryStream(Encoding.UTF8.GetBytes(dadosDasCategoriasEmCSV));

            _iIformFile.OpenReadStream().Returns(dadosDoArquivo);

            _iCategoriaServices.When(x => x.ImportCategoria(_iIformFile)).Do(
            x => { throw new ArgumentException("Erro ao importar dados das categorias.");});

            var controller = await _categoriaController.Import(_iIformFile) as ObjectResult;
            controller.Should().BeOfType<ObjectResult>();
            controller.StatusCode.Should().Be(500);
        }
    }
}

