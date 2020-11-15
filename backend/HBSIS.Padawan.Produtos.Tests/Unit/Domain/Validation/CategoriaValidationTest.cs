using FluentAssertions;
using FluentValidation;
using HBSIS.Padawan.Produtos.Application.Models.CategoriaModel;
using HBSIS.Padawan.Produtos.Application.Services.CategoriaServices;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using NSubstitute;
using System;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Domain.Validation
{
    public class CategoriaValidationTest
    {
        private readonly ICategoriaServices _categoriaService;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaValidationTest()
        {
            _categoriaRepository = Substitute.For<ICategoriaRepository>();
            _categoriaService = new CategoriaServices(_categoriaRepository);
        }

        [Theory]
        [InlineData(31)]
        [InlineData(2)]
        [InlineData(null)]
        public void Validar_NomeCategoria_Caracteres(int? qtdCaracteres)
        {
            string nomeCategoria = "";
            if (qtdCaracteres != null)
            {
                nomeCategoria = new string('a', Convert.ToInt32(qtdCaracteres));
            }

            var categoria = new CategoriaRequestModel()
            {
                NomeCategoria = nomeCategoria,
                FornecedorId = 4,
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _categoriaService.Create(categoria));
            if (qtdCaracteres == null)
            {
                ex.Result.Message.Should().Contain("Nome da categoria inválida.");
            }
            else if (qtdCaracteres == 31)
            {
                ex.Result.Message.Should().Contain("Nome da categoria deve conter no máximo 30 caracteres.");
            }
            else if (qtdCaracteres == 2)
            {
                ex.Result.Message.Should().Contain("Nome da categoria deve conter no mínimo 3 caracteres.");
            }
        }

        [Theory]
        [InlineData(-1)]
        public void Validar_FornecedorID(int Id)
        {
            string nomeCategoria = "bebida";
            var categoria = new CategoriaRequestModel()
            {
                NomeCategoria = nomeCategoria,
                FornecedorId = Id,
            };
        
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _categoriaService.Create(categoria));
            if (Id == -1)
            {
                ex.Result.Message.Should().Contain("FornecedorId deve ser maior que 0.");
            }
        }
    }
}
