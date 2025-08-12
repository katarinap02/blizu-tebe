using BlizuTebe.Dtos;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;


namespace BlizuTebe.Controllers
{
    [Route("api/users")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getById/{id}")]
        public ActionResult<UserDto> getUserById([FromRoute] long id)
        {
            var user = _userService.getById(id);
            return CreateResponse(user);
        }
    }
}
