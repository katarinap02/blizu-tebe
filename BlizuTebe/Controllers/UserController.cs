using BlizuTebe.Dtos;
using BlizuTebe.Services;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getById/{id}")]
        public ActionResult<UserDto> getUserById([FromRoute] long id)
        {
            var user = _userService.GetById(id);
            return CreateResponse(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("verify/{id}")]
        public ActionResult<UserDto> VerifyUser(long id)
        {
            var result = _userService.VerifyUser(id);
            return CreateResponse(result);
        }

        [HttpPost("register")]
        public ActionResult<UserDto> Register([FromForm] UserDto dto)
        {
            var result = _userService.Register(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            var result = _userService.GetAll();
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<UserDto> DeleteUser([FromRoute] long id)
        {
            var result = _userService.DeleteById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPut("{id}")]
        public ActionResult<UserDto> UpdateUser([FromRoute] long id, [FromForm] UserViewDto userDto)
        {
            var result = _userService.UpdateUser(id, userDto);
            return CreateResponse(result);
        }





    }
}
