using BlizuTebe.Dtos;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/communityRequest")]
    public class CommunityRequestController : BaseApiController
    {
        private readonly ICommunityRequestService _communityRequestService;

        public CommunityRequestController(ICommunityRequestService communityRequestService)
        {
            _communityRequestService = communityRequestService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<CommunityRequestDto> CreateCommunityRequest([FromForm] CommunityRequestDto dto)
        {
            var result = _communityRequestService.Create(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPut("{id}")]
        public ActionResult<CommunityRequestDto> UpdateCommunityRequest([FromRoute] long id, [FromForm] CommunityRequestDto dto)
        {
            var result = _communityRequestService.UpdateById(id, dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet]
        public ActionResult<List<CommunityRequestDto>> GetAllCommunityRequests()
        {
            var result = _communityRequestService.GetAll();
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<CommunityRequestDto> DeleteCommunityRequest([FromRoute] long id)
        {
            var result = _communityRequestService.DeleteById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getById/{id}")]
        public ActionResult<CommunityRequestDto> GetCommunityRequestById([FromRoute] long id)
        {
            var result = _communityRequestService.GetById(id);
            return CreateResponse(result);
        }
    }

}
