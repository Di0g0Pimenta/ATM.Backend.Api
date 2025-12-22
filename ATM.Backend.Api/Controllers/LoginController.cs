using Microsoft.AspNetCore.Mvc;

namespace ATM.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        // Endpoint de teste
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API ATM a funcionar");
        }
    }
}
