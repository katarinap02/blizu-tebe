using BlizuTebe.Dtos;
using BlizuTebe.Services;
using BlizuTebe.Services.Interfaces;
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

        [HttpPost]
        public IActionResult Create([FromBody] HelpRequestDto dto)
        {
            var result = helpRequestService.Create(dto);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] HelpRequestUpdateDto dto)
        {
            dto.Id = id;
            var result = helpRequestService.Update(dto);
            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = helpRequestService.Delete(id);
            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }


        [HttpGet("pending-requests")]
        public IActionResult GetPendingRequests()
        {
            var result = helpRequestService.GetPendingRequests();
            return Ok(result.Value);
        }

        [HttpGet("completed-requests")]
        public IActionResult GetCompletedRequests()
        {
            var result = helpRequestService.GetCompletedRequests();
            return Ok(result.Value);
        }

        [HttpGet("pending-offers")]
        public IActionResult GetPendingOffers()
        {
            var result = helpRequestService.GetPendingOffers();
            return Ok(result.Value);
        }

        [HttpGet("completed-offers")]
        public IActionResult GetCompletedOffers()
        {
            var result = helpRequestService.GetCompletedOffers();
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id) {
            var result = helpRequestService.GetById(id);
            if(result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }
        
    }
}
