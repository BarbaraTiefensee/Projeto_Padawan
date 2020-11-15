using HBSIS.Padawan.Produtos.Domain.Entities;

namespace HBSIS.Padawan.Produtos.Application.Models
{
    public class FornecedorResponseModel : FornecedorModelBase
    {
        public int  Id { get; set; }
        public FornecedorResponseModel(FornecedorEntity fornecedor)
        {
            RazaoSocial = fornecedor.RazaoSocial;
            NomeFantasia = fornecedor.NomeFantasia;
            Email = fornecedor.Email;
            Telefone = fornecedor.Telefone;
            CNPJ = fornecedor.CNPJ;
            Id = fornecedor.Id;

            Endereco = new EnderecoModel
            {
                Rua = fornecedor.Endereco.Rua,
                Cidade = fornecedor.Endereco.Cidade,
                CEP = fornecedor.Endereco.CEP,
                Bairro = fornecedor.Endereco.Bairro,
                Numero = fornecedor.Endereco.Numero,
                Complemento = fornecedor.Endereco.Complemento,
                UF = fornecedor.Endereco.UF
            };
        }
    }
}
