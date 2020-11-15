using HBSIS.Padawan.Produtos.Domain.builder;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBSIS.Padawan.Produtos.Domain.ComplexType
{
    [ComplexType]
    public class Endereco 
    {
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string UF { get; set; }

        public Endereco(FornecedorBuilder builder)
        {
            Rua = builder.Rua;
            Bairro = builder.Bairro;
            Cidade = builder.Cidade;
            CEP = builder.CEP;
            Numero = builder.Numero;
            Complemento = builder.Complemento;
            UF = builder.UF;
        }

        public Endereco()
        {
        }
    }
}
