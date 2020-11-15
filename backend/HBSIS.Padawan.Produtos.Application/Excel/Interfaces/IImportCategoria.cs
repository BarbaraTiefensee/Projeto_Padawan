using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HBSIS.Padawan.Produtos.Application.Excel.Interfaces
{
    public interface IImportCategoria
    {
        public IEnumerable<string> Importar(IFormFile arquivo);
    }
}
