using BlizuTebe.Dtos;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/discussion")]
    public class DiscussionController : BaseApiController
    {
        private readonly IDiscussionService _discussionService;

        public DiscussionController(IDiscussionService discussionService)
        {
            _discussionService = discussionService;
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPost]
        public ActionResult<DiscussionDto> CreateDiscussion([FromBody] DiscussionDto dto)
        {
            var result = _discussionService.Create(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPut("{id}")]
        public ActionResult<DiscussionDto> UpdateDiscussion([FromRoute] long id, [FromBody] DiscussionDto dto)
        {
            var result = _discussionService.UpdateById(id, dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet]
        public ActionResult<List<DiscussionDto>> GetAllDiscussions()
        {
            var result = _discussionService.GetAll();
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<DiscussionDto> DeleteDiscussion([FromRoute] long id)
        {
            var result = _discussionService.DeleteById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getById/{id}")]
        public ActionResult<DiscussionDto> GetDiscussionById([FromRoute] long id)
        {
            var result = _discussionService.GetById(id);
            return CreateResponse(result);
        }
    }

}
