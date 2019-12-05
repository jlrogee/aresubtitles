using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController: ControllerBase
    {
        [HttpPost("token")]
        public async Task<string> Get()
        {
            return "okboomer";
        }
    }
}