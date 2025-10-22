using BlizuTebe.Dtos;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/communityRequestUsers")]
    public class CommunityRequestUsersController : BaseApiController
    {
        private readonly ICommunityRequestUsersService _communityRequestUsersService;

        public CommunityRequestUsersController(ICommunityRequestUsersService communityRequestUsersService)
        {
            _communityRequestUsersService = communityRequestUsersService;
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet]
        public ActionResult<List<CommunityRequestUsersDto>> GetAllCommunityRequestUsers()
        {
            var result = _communityRequestUsersService.GetAll();
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getByUserId/{userId}")]
        public ActionResult<List<CommunityRequestUsersDto>> GetByUserId([FromRoute] long userId)
        {
            var result = _communityRequestUsersService.GetByUserId(userId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getByRequestId/{requestId}")]
        public ActionResult<List<CommunityRequestUsersDto>> GetByRequestId([FromRoute] long requestId)
        {
            var result = _communityRequestUsersService.GetByRequestId(requestId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPost]
        public ActionResult<CommunityRequestUsersDto> CreateCommunityRequestUsers([FromBody] CommunityRequestUsersDto dto)
        {
            var result = _communityRequestUsersService.Create(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpDelete("{userId}/{requestId}")]
        public ActionResult<CommunityRequestUsersDto> DeleteCommunityRequestUsers([FromRoute] long userId, [FromRoute] long requestId)
        {
            var result = _communityRequestUsersService.Delete(userId, requestId);
            return CreateResponse(result);
        }
    }
}
