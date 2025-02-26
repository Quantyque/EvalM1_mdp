using Microsoft.AspNetCore.Mvc;

namespace EvalM1_API_mdp.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly ILogger<PasswordController> _logger;

        [HttpGet("passwords")]
        public IActionResult GetAllPasswords()
        {
            _logger.LogInformation("GetAllPasswords action processed a request.");

            
            return Ok();
        }
    }
}
