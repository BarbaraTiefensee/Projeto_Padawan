using HBSIS.Padawan.Produtos.Domain.ComplexType;

namespace HBSIS.Padawan.Produtos.Application.Models
{
    public abstract class FornecedorModelBase
    {
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string NomeFantasia { get; set; }
        public Endereco Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Deletado { private get; set; }

        public FornecedorModelBase()
        {
            Endereco = new Endereco();
        }
    }
}
