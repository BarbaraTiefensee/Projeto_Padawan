using HBSIS.Padawan.Produtos.Application.Excel;
using HBSIS.Padawan.Produtos.Application.Models.CategoriaModel;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.CategoriaServices
{
    public class CategoriaServices : ICategoriaServices
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaServices(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Create(CategoriaRequestModel request)
        {
            var categoria = new CategoriaEntity(request.NomeCategoria, request.FornecedorId);
            var categoriaJaExiste = await _repository.VerificarSeJaExisteCategoria(categoria.NomeCategoria);

            if (categoriaJaExiste)
            {
                throw new ArgumentException("Categoria já existe.");
            }
            categoria.Validate();
            await _repository.Create(categoria);
            return categoria.Id;
        }

        public async Task<CategoriaEntity> Delete(int id)
        {
            var categoria = await _repository.GetById(id);
            if (categoria == null)
            {
                throw new ArgumentException("Id inexistente.");
            }
            categoria.Delete();
            await _repository.Delete(categoria);
            return categoria;
        }
        public async Task<CategoriaResponseModel> GetById(int id)
        {
            var categoria = await _repository.GetById(id);

            if (categoria == null)
            {
                throw new ArgumentException("Id da Categoria inexistente.");
            }

            var categoriaResponseModel = new CategoriaResponseModel(categoria);
            return categoriaResponseModel;
        }

        public async Task<IEnumerable<CategoriaResponseModel>> GetAll()
        {
            var categorias = await _repository.GetAll();
            return categorias.Select(categoria => CategoriaResponseModel.Converter(categoria));
        }

        public async Task<CategoriaEntity> Update(int id, CategoriaRequestModel request)
        {
            var categoria = await _repository.GetById(id);
            if (categoria == null)
            {
                throw new ArgumentException("Id da categoria inválida.");
            }

            categoria.Update(request.NomeCategoria, request.FornecedorId);
            categoria.Validate();

            var categoriaExiste = await _repository.VerificarSeExisteCategoriaComEsseNome(categoria.NomeCategoria, categoria.Id);
            if (categoriaExiste)
            {
                throw new ArgumentException("Nome da Categoria já existente.");
            }
            await _repository.Update(categoria);
            return categoria;
        }

        public async Task<string> ExportCategoria()
        {
            var categorias = await _repository.GetAllExportCsv();
            var exportacao = new ExportCsvCategoria();

            return exportacao.ExportarDadosCategoria(categorias);
        }

        public async Task<IEnumerable<CategoriaEntity>> ImportCategoria(IFormFile caminhoArquivo)
        {
            var importacao = new ImportCategoria();
            var categoriasImportadas = importacao.Importar(caminhoArquivo);
            var listaCategoria = new List<CategoriaEntity>();

            foreach (var categoria in categoriasImportadas)
            {
                var dadosCategoria = categoria.Split(new[] { ';' });

                if (dadosCategoria.Length > 1 && dadosCategoria[0] != null && dadosCategoria[1] != null)
                {
                    var categoriaJaExiste = await _repository.VerificarSeJaExisteCategoria(dadosCategoria[0]);
                    if (categoriaJaExiste)
                    {
                        throw new ArgumentException("A Categoria ja existe.");
                    }

                    var novaCategoria = new CategoriaEntity(dadosCategoria[0], int.Parse(dadosCategoria[1]));

                    await _repository.Create(novaCategoria);
                    listaCategoria.Add(novaCategoria);
                }
                else
                {
                    throw new ArgumentException("Dados do CSV inválidos.");
                }
            }

            if (!listaCategoria.Any())
            {
                throw new ArgumentException("Dados do CSV não encontrados.");
            }
            return listaCategoria;
        }
    }
}
