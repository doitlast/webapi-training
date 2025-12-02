using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace MESA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmitterController : Controller
    {
        private static readonly string[] EmitterList = new[]
        {
            "Emitter1", "Emitter2", "Emitter3", "Emitter4"
        };
        [HttpGet("GetAllEmitters")]
        public IActionResult GetAllEmitters()
        {
            return Ok(EmitterList);
        }
    }
}