using HBSIS.Padawan.Produtos.Application.Excel.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace HBSIS.Padawan.Produtos.Application.Excel
{
    public class ImportCategoria : IImportCategoria
    {
        public IEnumerable<string> Importar(IFormFile arquivo)
        {
            var dadosArquivo = new List<string>();
            if (arquivo.OpenReadStream() != null)
            {
                using (var reader = new StreamReader(arquivo.OpenReadStream()))
                {
                    reader.ReadLine();
                    while (reader.Peek() >= 0)
                    {
                        var linha = reader.ReadLine();
                        dadosArquivo.Add(linha);
                    }
                }
            }
            return dadosArquivo;
        }
    }
}
