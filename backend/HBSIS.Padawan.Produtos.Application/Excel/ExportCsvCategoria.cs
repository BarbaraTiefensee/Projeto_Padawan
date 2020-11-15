using HBSIS.Padawan.Produtos.Application.Excel.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Entities;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Application.Excel
{
    public class ExportCsvCategoria : IExportCategoria
    {
        public string ExportarDadosCategoria(IEnumerable<CategoriaEntity> categorias)
        {
            var categoriaBuilder = new StringBuilder();
            categoriaBuilder.AppendLine("NomeCategoria; NomeFantasia");

            foreach (var categoria in categorias)
            {
                categoriaBuilder.AppendLine($"{categoria.NomeCategoria};{categoria.Fornecedor.NomeFantasia}");
            }
            return categoriaBuilder.ToString();
        }
    }
}
