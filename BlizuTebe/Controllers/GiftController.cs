using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService giftService;

        public GiftController(IGiftService giftService)
        {
            this.giftService = giftService;
        }

        [HttpGet]
        public ActionResult GetAll([FromQuery] int page, [FromQuery] int size, [FromQuery] GiftCategory? category)
        {
            var result = giftService.GetAll(page, size, category);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("pending")]
        public ActionResult GetPending([FromQuery] int page, [FromQuery] int size, [FromQuery] GiftCategory? category)
        {
            var result = giftService.GetPending(page, size, category);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("completed")]
        public ActionResult GetCompleted([FromQuery] int page, [FromQuery] int size, [FromQuery] GiftCategory? category)
        {
            var result = giftService.GetCompleted(page, size, category);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(long id)
        {
            var result = giftService.GetById(id);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        public ActionResult Create([FromForm] GiftUpdateDto dto)
        {
            return Ok(giftService.Create(dto));
        }

        [HttpPut]
        public ActionResult Update([FromForm] GiftUpdateDto dto)
        {
            return Ok(giftService.Update(dto));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            return Ok(giftService.Delete(id));
        }
        
    }
}
