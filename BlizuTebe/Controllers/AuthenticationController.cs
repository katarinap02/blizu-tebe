using BlizuTebe.Dtos;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BlizuTebe.Controllers
{
    [Route("api/auth")]
    public class AuthenticationController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public ActionResult<AuthenticationTokensDto> Login([FromBody] CredentialsDto credentials)
        {
            var result = _authenticationService.Login(credentials);
            return CreateResponse(result);
        }
    }
}
