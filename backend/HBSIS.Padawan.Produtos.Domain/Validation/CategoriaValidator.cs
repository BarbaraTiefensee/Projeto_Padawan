using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;

namespace HBSIS.Padawan.Produtos.Domain.Validation
{
    public class CategoriaValidator : AbstractValidator<CategoriaEntity>
    {
        public CategoriaValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(c => c.NomeCategoria)
                .NotEmpty().WithMessage("Nome da categoria inválida.")
                .MinimumLength(3).WithMessage("Nome da categoria deve conter no mínimo 3 caracteres.")
                .MaximumLength(30).WithMessage("Nome da categoria deve conter no máximo 30 caracteres.");

            RuleFor(c => c.FornecedorId)
                .ExclusiveBetween(0, int.MaxValue).WithMessage("FornecedorId deve ser maior que 0.")
                .NotEmpty().WithMessage("FornecedorId inválido.");
        }
    }
}
