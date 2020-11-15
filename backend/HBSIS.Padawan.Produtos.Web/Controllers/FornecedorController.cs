using HBSIS.Padawan.Produtos.Application.Common;
using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorServices _fornecedorService;

        public FornecedorController(IFornecedorServices fornecedorServices)
        {
            _fornecedorService = fornecedorServices;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]FornecedorRequestModel fornecedorRequest)
        {
            try
            {
                int id = await _fornecedorService.Create(fornecedorRequest);
                return CreatedAtRoute(fornecedorRequest, id);
            }
            catch (FornecedorException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                return Ok(await _fornecedorService.GetById(id));
            }
            catch (FornecedorException ex)
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] FornecedorRequestModel model)
        {
            try
            {
                await _fornecedorService.Update(id, model);
                return Ok("Fornecedor Atualizado com sucesso.");
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
                await _fornecedorService.Delete(id);
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
    }
}