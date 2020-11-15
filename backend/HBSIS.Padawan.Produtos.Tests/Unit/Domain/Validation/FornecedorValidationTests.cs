using FluentAssertions;
using FluentValidation;
using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Models;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using NSubstitute;
using System;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Domain.Validation
{
    public class FornecedorValidationTests
    {
        private readonly IFornecedorServices _fornecedorService;
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorValidationTests()
        {
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();
            _fornecedorService = new FornecedorServices(_fornecedorRepository);
        }

        [Theory]
        [InlineData(51)]
        [InlineData(2)]
        [InlineData(null)]
        public void Validar_RazaoSocial_Caracteres(int? qtdCaracteres)
        {
            string razaoSocial = "";
            if (qtdCaracteres != null)
            {
                razaoSocial = new string('a', Convert.ToInt32(qtdCaracteres));
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = razaoSocial,
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = "74.358.222/0001-17",
                Telefone = "47991085945",
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (qtdCaracteres == null)
            {
                ex.Result.Message.Should().Contain("Razão Social não pode estar vazia.");
            }
            else if (qtdCaracteres == 51)
            {
                ex.Result.Message.Should().Contain("Razão Social não pode conter mais de 50 caracteres.");
            }
            else if (qtdCaracteres == 2)
            {
                ex.Result.Message.Should().Contain("Razão Social deve conter mais de 3 caracteres.");
            }
        }

        [Theory]
        [InlineData(51)]
        [InlineData(9)]
        [InlineData(null)]
        public void Validar_NomeFantasia_Caracteres(int? qtdCaracteres)
        {
            string nomeFantasia = "";
            if (qtdCaracteres != null)
            {
                nomeFantasia = new string('a', Convert.ToInt32(qtdCaracteres));
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = nomeFantasia,
                CNPJ = "74.358.222/0001-17",
                Telefone = "47991085945",
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (qtdCaracteres == null)
            {
                ex.Result.Message.Should().Contain("Nome Fantasia não pode estar vazio.");
            }
            else if (qtdCaracteres == 51)
            {
                ex.Result.Message.Should().Contain("Nome Fantasia não pode conter mais de 50 caracteres.");
            }
            else if (qtdCaracteres == 9)
            {
                ex.Result.Message.Should().Contain("Nome Fantasia deve conter mais de 10 caracteres.");
            }

        }

        [Theory]
        [InlineData("00000/0001-17")]
        [InlineData("x")]
        public void Validar_CNPJ_Caracteres(string cnpjTeste)
        {
            var cnpj = "";
            if (cnpjTeste != "x")
            {
                cnpj = cnpjTeste;
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = cnpj,
                Telefone = "47991085945",
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (cnpjTeste == "x")
            {
                ex.Result.Message.Should().Contain("Cnpj não pode estar vazio.");
            }
            else if (cnpjTeste == "00000/0001-17")
            {
                ex.Result.Message.Should().Contain("Cnpj incorreto.");
            }
        }

        [Theory]
        [InlineData(9)]
        [InlineData(12)]
        [InlineData(null)]
        public void Validar_Telefone_Caracteres(int? qtdCaracteres)
        {
            string telefone = "";
            if (qtdCaracteres != null)
            {
                telefone = new string('t', Convert.ToInt32(qtdCaracteres));
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = "74.358.222/0001-17",
                Telefone = telefone,
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (qtdCaracteres == null)
            {
                ex.Result.Message.Should().Contain("Telefone não pode estar vazio.");
            }
            else if (qtdCaracteres == 9)
            {
                ex.Result.Message.Should().Contain("Telefone deve conter exatamente 10 caracteres.");
            }
            else if (qtdCaracteres == 12)
            {
                ex.Result.Message.Should().Contain("Telefone deve conter exatamente 11 caracteres.");
            }
        }

        [Theory]
        [InlineData("mabelksouza.com")]
        [InlineData("x")]
        public void Validar_Email_Caracteres(string emailTeste)
        {
            string email = "";
            if (emailTeste != "x")
            {
                email = emailTeste;
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = "74.358.222/0001-17",
                Telefone = "47991085945",
                Email = email,
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (emailTeste == "x")
            {
                ex.Result.Message.Should().Contain("Email não pode ser vazio.");
            }
            else if (emailTeste == "mabelksouza.com")
            {
                ex.Result.Message.Should().Contain("Email incorreto. EX:exemplo@gmail.com.");
            }
        }

        [Theory]
        [InlineData(9)]
        [InlineData(81)]
        [InlineData(null)]
        public void Validar_Rua_Caracteres(int? qtdCaracteres)
        {
            string rua = "";
            if (qtdCaracteres != null)
            {
                rua = new string('r', Convert.ToInt32(qtdCaracteres));
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = rua,
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = "74.358.222/0001-17",
                Telefone = "47991085945",
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (qtdCaracteres == null)
            {
                ex.Result.Message.Should().Contain("Rua não pode ser vazia.");
            }
            else if (qtdCaracteres == 9)
            {
                ex.Result.Message.Should().Contain("Rua não pode ter menos de 10 caracteres.");
            }
            else if (qtdCaracteres == 81)
            {
                ex.Result.Message.Should().Contain("Rua não pode ter mais de 80 caracteres.");
            }
        }

        [Theory]
        [InlineData(3)]
        [InlineData(41)]
        [InlineData(null)]
        public void Validar_Cidade_Caracteres(int? qtdCaracteres)
        {
            string cidade = "";
            if (qtdCaracteres != null)
            {
                cidade = new string('c', Convert.ToInt32(qtdCaracteres));
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = cidade,
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = "74.358.222/0001-17",
                Telefone = "47991085945",
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (qtdCaracteres == null)
            {
                ex.Result.Message.Should().Contain("Cidade não pode ser vazia.");
            }
            else if (qtdCaracteres == 3)
            {
                ex.Result.Message.Should().Contain("Cidade não pode ter menos de 4 caracteres.");
            }
            else if (qtdCaracteres == 41)
            {
                ex.Result.Message.Should().Contain("Cidade não pode ter mais de 40 caracteres.");
            }
        }

        [Theory]
        [InlineData(6)]
        [InlineData(51)]
        [InlineData(null)]
        public void Validar_Bairro_Caracteres(int? qtdCaracteres)
        {
            string bairro = "";
            if (qtdCaracteres != null)
            {
                bairro = new string('b', Convert.ToInt32(qtdCaracteres));
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = "Blumenau",
                    Bairro = bairro,
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = "74.358.222/0001-17",
                Telefone = "47991085945",
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (qtdCaracteres == null)
            {
                ex.Result.Message.Should().Contain("Bairro não pode ser vazio.");
            }
            else if (qtdCaracteres == 6)
            {
                ex.Result.Message.Should().Contain("Bairro não pode ter menos de 7 caracteres.");
            }
            else if (qtdCaracteres == 51)
            {
                ex.Result.Message.Should().Contain("Bairro não pode ter mais de 50 caracteres.");
            }
        }

        [Theory]
        [InlineData(7)]
        [InlineData(9)]
        [InlineData(null)]
        public void Validar_CEP_Caracteres(int? qtdCaracteres)
        {
            string cep = "";
            if (qtdCaracteres != null)
            {
                cep = new string('c', Convert.ToInt32(qtdCaracteres));
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = cep,
                    Numero = "270",
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = "74.358.222/0001-17",
                Telefone = "47991085945",
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (qtdCaracteres == null)
            {
                ex.Result.Message.Should().Contain("Cep não pode ser vazio.");
            }
            else if (qtdCaracteres == 7)
            {
                ex.Result.Message.Should().Contain("Cep não teve ter menos 8 caracteres.");
            }
            else if (qtdCaracteres == 9)
            {
                ex.Result.Message.Should().Contain("Cep deve conter exatamente 8 caracteres.");
            }
        }

        [Theory]
        [InlineData(7)]
        [InlineData(null)]
        public void Validar_Numero_Caracteres(int? qtdCaracteres)
        {
            string numero = "";
            if (qtdCaracteres != null)
            {
                numero = new string('n', Convert.ToInt32(qtdCaracteres));
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = numero,
                    Complemento = "502",
                    UF = "SC",
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = "74.358.222/0001-17",
                Telefone = "47991085945",
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (qtdCaracteres == null)
            {
                ex.Result.Message.Should().Contain("Número não pode ser vazio.");
            }
            else if (qtdCaracteres == 7)
            {
                ex.Result.Message.Should().Contain("Numero não pode ter mais de 6 caracteres.");
            }
        }

        [Theory]
        [InlineData(71)]
        [InlineData(null)]
        public void Validar_Complemento_Caracteres(int? qtdCaracteres)
        {
            string complemento = "";
            complemento = new string('c', Convert.ToInt32(qtdCaracteres));

            if (qtdCaracteres != null)
            {
                complemento = new string('n', Convert.ToInt32(qtdCaracteres));
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = complemento,
                    UF = "SC",
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = "74.358.222/0001-17",
                Telefone = "47991085945",
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (qtdCaracteres == 71)
            {
                ex.Result.Message.Should().Contain("Complemento não pode ter mais de 70 caracteres.");
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(null)]
        public void Validar_UF_Caracteres(int? qtdCaracteres)
        {
            string uf = "";
            if (qtdCaracteres != null)
            {
                uf = new string('f', Convert.ToInt32(qtdCaracteres));
            }

            var fornecedor = new FornecedorRequestModel()
            {
                Endereco = new EnderecoTests()
                {
                    Rua = "Rua Paraguay",
                    Cidade = "Blumenau",
                    Bairro = "Ponta Aguda",
                    CEP = "89050020",
                    Numero = "270",
                    Complemento = "502",
                    UF = uf,
                },

                RazaoSocial = "Barbara Comesticos",
                NomeFantasia = "Barbaras Comesticos e Batom",
                CNPJ = "74.358.222/0001-17",
                Telefone = "47991085945",
                Email = "barbara@gmail.com",
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _fornecedorService.Create(fornecedor));
            if (qtdCaracteres == null)
            {
                ex.Result.Message.Should().Contain("UF não pode ser vazio.");
            }
            else if (qtdCaracteres == 1)
            {
                ex.Result.Message.Should().Contain("UF deve conter exatamente 2 caracteres.");
            }
            else if (qtdCaracteres == 3)
            {
                ex.Result.Message.Should().Contain("UF deve conter exatamente 2 caracteres.");
            }
        }
    }
}
