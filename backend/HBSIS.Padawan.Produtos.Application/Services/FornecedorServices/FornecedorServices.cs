using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Models;
using HBSIS.Padawan.Produtos.Domain.builder;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services
{
    public class FornecedorServices : IFornecedorServices
    {
        private readonly IFornecedorRepository _repository;

        public FornecedorServices(IFornecedorRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Create(FornecedorRequestModel request)
        {
            var fornecedor = new FornecedorBuilder()
                .ComRazaoSocial(request.RazaoSocial)
                .ComNomeFantasia(request.NomeFantasia)
                .ComTelefone(request.Telefone)
                .ComEmail(request.Email)
                .ComCNPJ(request.CNPJ)
                .ComRua(request.Endereco.Rua)
                .ComNumero(request.Endereco.Numero)
                .ComComplemento(request.Endereco.Complemento)
                .ComBairro(request.Endereco.Bairro)
                .ComCEP(request.Endereco.CEP)
                .ComCidade(request.Endereco.Cidade)
                .ComUF(request.Endereco.UF)
                .Build();

            fornecedor.Validate();
            var fornecedorJaExiste = await _repository.VerificarSeFornecedorJaExiste(fornecedor.CNPJ);

            if (fornecedorJaExiste)
            {
                throw new ArgumentException("Fornecedor já existe.");
            }
            await _repository.Create(fornecedor);
            return fornecedor.Id;
        }

        public async Task<FornecedorEntity> Delete(int id)
        {
            var fornecedor = await _repository.GetById(id);

            if (fornecedor == null)
            {
                throw new ArgumentException("Id inexistente.");
            }

            fornecedor.Delete();
            await _repository.Delete(fornecedor);
            return fornecedor;
        }

        public async Task<FornecedorResponseModel> GetById(int id)
        {
            var fornecedor = await _repository.GetById(id);

            if (fornecedor == null)
            {
                throw new ArgumentException("Id do fornecedor inexistente.");
            }

            var fornecedorResponseModel = new FornecedorResponseModel(fornecedor);
            return fornecedorResponseModel;
        }

        public async Task<FornecedorEntity> Update(int id, FornecedorRequestModel request)
        {
            var fornecedor = await _repository.GetById(id);
            if (fornecedor == null)
            {
                throw new ArgumentException("Id invalido.");
            }

            var fornecedorRequestModel = new FornecedorBuilder()
               .ComRazaoSocial(request.RazaoSocial)
               .ComNomeFantasia(request.NomeFantasia)
               .ComTelefone(request.Telefone)
               .ComEmail(request.Email)
               .ComCNPJ(request.CNPJ)
               .ComRua(request.Endereco.Rua)
               .ComNumero(request.Endereco.Numero)
               .ComComplemento(request.Endereco.Complemento)
               .ComBairro(request.Endereco.Bairro)
               .ComCEP(request.Endereco.CEP)
               .ComCidade(request.Endereco.Cidade)
               .ComUF(request.Endereco.UF);

            fornecedor.Update(fornecedorRequestModel);
            fornecedor.Validate();

            var verificandoCnpj = await _repository.ExisteFornecedorComEsseCnpj(fornecedor.CNPJ, fornecedor.Id);
            if (verificandoCnpj)
            {
                throw new ArgumentException("Fornecedor não poderá ser cadastrado.");
            }
            await _repository.Update(fornecedor);
            return fornecedor;
        }
    }
}
