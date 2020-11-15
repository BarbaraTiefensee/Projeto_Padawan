using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.builder;
using HBSIS.Padawan.Produtos.Domain.ComplexType;
using HBSIS.Padawan.Produtos.Domain.Validation;
using System.Collections.Generic;

namespace HBSIS.Padawan.Produtos.Domain.Entities
{
    public class FornecedorEntity : BaseEntity
    {
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string NomeFantasia { get; set; }
        public Endereco Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<CategoriaEntity> Categorias { get; set; }

        public static FornecedorEntity Of(FornecedorBuilder builder) => new FornecedorEntity
        {
            RazaoSocial = builder.RazaoSocial,
            CNPJ = builder.CNPJ,
            NomeFantasia = builder.NomeFantasia,
            Telefone = builder.Telefone,
            Email = builder.Email,
            Endereco = new Endereco(builder)
        };

        public FornecedorEntity()
        {
        }

        public void Update(FornecedorBuilder builder)
        {
            RazaoSocial = builder.RazaoSocial;
            CNPJ = builder.CNPJ;
            NomeFantasia = builder.NomeFantasia;
            Telefone = builder.Telefone;
            Email = builder.Email;
            Endereco = new Endereco(builder);
        }
    
        public void Delete()
        {
            this.Deletado = true;
        }

        public void Validate()
        {
            var fornecedorValidator = new FornecedorValidator();
            fornecedorValidator.ValidateAndThrow(this);
        }
    }
}

