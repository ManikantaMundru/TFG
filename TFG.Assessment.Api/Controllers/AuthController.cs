using Microsoft.AspNetCore.Mvc;
using TFG.Assessment.Api.Interfaces;
using TFG.Assessment.Domain.Requests;
using TFG.Assessment.Domain.Response;

namespace TFG.Assessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("authenticate")]
        public ActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            try
            {
                if (request == null)
                {
                    return Unauthorized();
                }
                var token = _tokenService.Authenticate(request);

                return Ok(token);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
