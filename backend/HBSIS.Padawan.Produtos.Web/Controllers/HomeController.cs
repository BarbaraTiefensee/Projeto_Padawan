using Microsoft.AspNetCore.Mvc;

namespace HBSIS.Padawan.Produtos.Web.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ok");
        }
    }
}
