using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Services;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpRequestController : ControllerBase
    {
        private readonly IHelpRequestService helpRequestService;

        public HelpRequestController(IHelpRequestService _helpRequestService)
        {
            helpRequestService = _helpRequestService;
        }


        [Authorize(Roles = "Admin,Member")]
        [HttpPost]
        public IActionResult Create([FromForm] HelpRequestUpdateDto dto)
        {
            var result = helpRequestService.Create(dto);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            
            return Ok(result.Value);
        }


        [Authorize(Roles = "Admin,Member")]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromForm] HelpRequestUpdateDto dto)
        {
            dto.Id = id;
            var result = helpRequestService.Update(dto);
            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = helpRequestService.Delete(id);
            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("pending-requests")]
        public IActionResult GetPendingRequests()
        {
            var result = helpRequestService.GetPending(Models.HelpType.Asking);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("completed-requests")]
        public IActionResult GetCompletedRequests()
        {
            var result = helpRequestService.GetCompleted(Models.HelpType.Asking);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("pending-offers")]
        public IActionResult GetPendingOffers()
        {
            var result = helpRequestService.GetPending(Models.HelpType.Offering);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("completed-offers")]
        public IActionResult GetCompletedOffers()
        {
            var result = helpRequestService.GetCompleted(Models.HelpType.Offering);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("{id}")]
        public IActionResult Get(long id) {
            var result = helpRequestService.GetById(id);
            if(result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("category")]
        public IActionResult GetByCategory(
                [FromQuery] HelpCategory category,
                [FromQuery] HelpType helpType)
        {
            var result = helpRequestService.GetByCategory(helpType, category);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("{userId}/expired")]
        public IActionResult GetMyExpired(long userId, [FromQuery] HelpType type)
        {
            var result = helpRequestService.GetMyExpired(type, userId);
            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("match/{helpId}")]
        public IActionResult GetMatching(long helpId)
        {
            var result = helpRequestService.MatchRequestAndOffer(helpId);
            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
