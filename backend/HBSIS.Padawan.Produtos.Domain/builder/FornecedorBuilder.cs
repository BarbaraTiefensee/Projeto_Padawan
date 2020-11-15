using HBSIS.Padawan.Produtos.Domain.Entities;

namespace HBSIS.Padawan.Produtos.Domain.builder
{
    public class FornecedorBuilder : BaseEntity
    {
        internal string RazaoSocial { get; private set; }
        internal string NomeFantasia { get; private set; }
        internal string Telefone { get; private set; }
        internal string Email { get; private set; }
        internal string CNPJ { get; private set; }
        internal string Rua { get; private set; }
        internal string Numero { get; private set; }
        internal string Complemento { get; private set; }
        internal string Bairro { get; private set; }
        internal string CEP { get; private set; }
        internal string Cidade { get; private set; }
        internal string UF { get; private set; }

        public FornecedorEntity Build()
        {
            var entity = FornecedorEntity.Of(this);
            entity.Id = Id;
            return entity;
        }

        public FornecedorBuilder ComRazaoSocial(string razaoSocial)
        {
            RazaoSocial = razaoSocial;
            return this;
        }

        public FornecedorBuilder ComNomeFantasia(string nomeFantasia)
        {
            NomeFantasia = nomeFantasia;
            return this;
        }

        public FornecedorBuilder ComTelefone(string telefone)
        {
            Telefone = telefone;
            return this;
        }

        public FornecedorBuilder ComEmail(string email)
        {
            Email = email;
            return this;
        }

        public FornecedorBuilder ComCNPJ(string cnpj)
        {
            CNPJ = cnpj;
            return this;
        }

        public FornecedorBuilder ComRua(string rua)
        {
            Rua = rua;
            return this;
        }

        public FornecedorBuilder ComNumero(string numero)
        {
            Numero = numero;
            return this;
        }

        public FornecedorBuilder ComComplemento(string complemento)
        {
            Complemento = complemento;
            return this;
        }

        public FornecedorBuilder ComBairro(string bairro)
        {
            Bairro = bairro;
            return this;
        }

        public FornecedorBuilder ComCEP(string cep)
        {
            CEP = cep;
            return this;
        }

        public FornecedorBuilder ComCidade(string cidade)
        {
            Cidade = cidade;
            return this;
        }

        public FornecedorBuilder ComUF(string uf)
        {
            UF = uf;
            return this;
        }

        public FornecedorBuilder Deletar()
        {
            Deletado = false;
            return this;
        }

        public FornecedorBuilder ComId(int id)
        {
            Id = id;
            return this;
        }
    }
}
