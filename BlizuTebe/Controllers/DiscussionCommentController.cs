using BlizuTebe.Dtos;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/discussionComment")]
    public class DiscussionCommentController : BaseApiController
    {
        private readonly IDiscussionCommentService _discussionCommentService;

        public DiscussionCommentController(IDiscussionCommentService discussionCommentService)
        {
            _discussionCommentService = discussionCommentService;
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPost]
        public ActionResult<DiscussionCommentDto> CreateDiscussionComment([FromBody] DiscussionCommentDto dto)
        {
            var result = _discussionCommentService.Create(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPut("{id}")]
        public ActionResult<DiscussionCommentDto> UpdateDiscussionComment([FromRoute] long id, [FromBody] DiscussionCommentDto dto)
        {
            var result = _discussionCommentService.UpdateById(id, dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet]
        public ActionResult<List<DiscussionCommentDto>> GetAllDiscussionComments()
        {
            var result = _discussionCommentService.GetAll();
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<DiscussionCommentDto> DeleteDiscussionComment([FromRoute] long id)
        {
            var result = _discussionCommentService.DeleteById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getById/{id}")]
        public ActionResult<DiscussionCommentDto> GetDiscussionCommentById([FromRoute] long id)
        {
            var result = _discussionCommentService.GetById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getByDiscussionId/{discussionId}")]
        public ActionResult<List<DiscussionCommentDto>> GetByDiscussionId([FromRoute] long discussionId)
        {
            var result = _discussionCommentService.GetByDiscussionId(discussionId);
            return CreateResponse(result);
        }
    }
}
