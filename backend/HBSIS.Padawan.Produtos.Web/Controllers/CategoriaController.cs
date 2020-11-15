using HBSIS.Padawan.Produtos.Application.Common;
using HBSIS.Padawan.Produtos.Application.Models.CategoriaModel;
using HBSIS.Padawan.Produtos.Application.Services.CategoriaServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServices _categoriaServices;

        public CategoriaController(ICategoriaServices categoriaServices)
        {
            _categoriaServices = categoriaServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CategoriaRequestModel categoriaRequest)
        {
            try
            {
                int id = await _categoriaServices.Create(categoriaRequest);
                return CreatedAtRoute(categoriaRequest, id);
            }
            catch (CategoriaException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _categoriaServices.GetAll());
            }
            catch (CategoriaException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            try
            {
                return Ok(await _categoriaServices.GetById(id));
            }
            catch (CategoriaException ex)
            {
                return NotFound(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoriaRequestModel model)
        {
            try
            {
                await _categoriaServices.Update(id, model);
                return Ok("Categoria atualizada com sucesso.");
            }
            catch (FornecedorException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _categoriaServices.Delete(id);
                return Ok("Deletado com sucesso.");
            }
            catch (FornecedorException gex)
            {
                return BadRequest(gex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("csv")]
        [HttpGet]
        public async Task<FileContentResult> Csv()
        {
            try
            {
                return File(Encoding.UTF8.GetBytes(await _categoriaServices.ExportCategoria()), "text/csv", "Categorias.csv");
            }
            catch (Exception)
            {
                throw new Exception("Erro ao exportar dados para CSV.");
            }
        }

        [Route("import")]
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile arquivo)
        {
            try
            {
                await _categoriaServices.ImportCategoria(arquivo);
                return Ok("Categorias importadas com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao importar dados das categorias.");
            }
        }
    }
}
