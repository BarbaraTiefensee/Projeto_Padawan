using HBSIS.Padawan.Produtos.Domain.Entities;
using System.Collections.Generic;

namespace HBSIS.Padawan.Produtos.Application.Excel.Interfaces
{
    public interface IExportCategoria
    {
        string ExportarDadosCategoria(IEnumerable<CategoriaEntity> categoria);
    }
}
