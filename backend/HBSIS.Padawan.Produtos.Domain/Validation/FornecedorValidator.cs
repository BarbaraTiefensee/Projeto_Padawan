using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Text.RegularExpressions;

namespace HBSIS.Padawan.Produtos.Domain.Validation
{
    public class FornecedorValidator : AbstractValidator<FornecedorEntity>
    {
        public FornecedorValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(f => f.RazaoSocial)
                .NotNull().WithMessage("Razão Social não pode ser nula.")
                .NotEmpty().WithMessage("Razão Social não pode estar vazia.")
                .MinimumLength(3).WithMessage("Razão Social deve conter mais de 3 caracteres.")
                .MaximumLength(50).WithMessage("Razão Social não pode conter mais de 50 caracteres.");

            RuleFor(f => f.NomeFantasia)
                .NotNull().WithMessage("Nome Fantasia não pode ser nula.")
                .NotEmpty().WithMessage("Nome Fantasia não pode estar vazio.")
                .MinimumLength(10).WithMessage("Nome Fantasia deve conter mais de 10 caracteres.")
                .MaximumLength(50).WithMessage("Nome Fantasia não pode conter mais de 50 caracteres.");

            RuleFor(f => f.Email)
                .NotNull().WithMessage("Email não pode ser nulo.")
                .NotEmpty().WithMessage("Email não pode ser vazio.")
                .EmailAddress().WithMessage("Email incorreto. EX:exemplo@gmail.com.")
                .MaximumLength(60).WithMessage("Emaila não pode conter mais de 60 caracteres.");


            RuleFor(f => f.CNPJ)
                .NotNull().WithMessage("Cnpj não pode ser nulo.")
                .NotEmpty().WithMessage("Cnpj não pode estar vazio.")
                .Must(ValidateCnpj).WithMessage("Cnpj incorreto.");

            RuleFor(f => f.Endereco.Rua)
                .NotNull().WithMessage("Rua não pode ser nula.")
                .NotEmpty().WithMessage("Rua não pode ser vazia.")
                .MinimumLength(10).WithMessage("Rua não pode ter menos de 10 caracteres.")
                .MaximumLength(80).WithMessage("Rua não pode ter mais de 80 caracteres.");

            RuleFor(f => f.Endereco.Bairro)
                .NotNull().WithMessage("Bairro não pode ser nulo.")
                .NotEmpty().WithMessage("Bairro não pode ser vazio.")
                .MinimumLength(7).WithMessage("Bairro não pode ter menos de 7 caracteres.")
                .MaximumLength(50).WithMessage("Bairro não pode ter mais de 50 caracteres.");

            RuleFor(f => f.Endereco.CEP)
                .NotNull().WithMessage("Cep não pode ser nulo.")
                .NotEmpty().WithMessage("Cep não pode ser vazio.")
                .MinimumLength(8).WithMessage("Cep não teve ter menos 8 caracteres.")
                .MaximumLength(8).WithMessage("Cep deve conter exatamente 8 caracteres.");

            RuleFor(f => f.Endereco.Cidade)
                .NotNull().WithMessage("Cidade não pode ser nula.")
                .NotEmpty().WithMessage("Cidade não pode ser vazia.")
                .MinimumLength(4).WithMessage("Cidade não pode ter menos de 4 caracteres.")
                .MaximumLength(40).WithMessage("Cidade não pode ter mais de 40 caracteres.");

            RuleFor(f => f.Endereco.Complemento)
                .MaximumLength(70).WithMessage("Complemento não pode ter mais de 70 caracteres.");

            RuleFor(f => f.Endereco.Numero)
                .NotNull().WithMessage("Número não pode ser nulo.")
                .NotEmpty().WithMessage("Número não pode ser vazio.")
                .MaximumLength(6).WithMessage("Numero não pode ter mais de 6 caracteres.");

            RuleFor(f => f.Endereco.UF)
                .NotNull().WithMessage("UF não pode ser nulo.")
                .NotEmpty().WithMessage("UF não pode ser vazio.")
                .MinimumLength(2).WithMessage("UF deve conter exatamente 2 caracteres.")
                .MaximumLength(2).WithMessage("UF deve conter exatamente 2 caracteres.");


            RuleFor(f => f.Telefone)
                .NotNull().WithMessage("Telefone não pode ser nulo.")
                .NotEmpty().WithMessage("Telefone não pode estar vazio.")
                .MinimumLength(10).WithMessage("Telefone deve conter exatamente 10 caracteres.")
                .MaximumLength(11).WithMessage("Telefone deve conter exatamente 11 caracteres.");

        }

        private bool ValidateCnpj(string cnpj)
        {
            if (cnpj == null)
                throw new ValidationException("CNPJ é necessario.");

            string digito1, digito2, cnpjSemDigito;

            cnpj = Regex.Replace(cnpj, "[^0-9]", string.Empty);

            if (cnpj.Length != 14)
                return false;

            cnpjSemDigito = cnpj.Substring(0, 12);

            digito1 = Modulo11(cnpjSemDigito, 0);
            cnpjSemDigito = cnpjSemDigito + digito1;
            digito2 = digito1 + Modulo11(cnpjSemDigito, 1);

            return cnpj.EndsWith(digito2);

        }

        private string Modulo11(string cnpj, int posicao)
        {
            int soma, resto;
            soma = 0;
            string multiplicador = "6543298765432";

            for (int i = 0; i < cnpj.Length; i++)
                soma += int.Parse(cnpj[i].ToString()) * Convert.ToInt32(multiplicador.Substring( i + 1 - posicao,1));

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            return resto.ToString();
        }
    }
}
