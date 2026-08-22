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
            return Ok(giftService.GetAll(page, size, category));
        }

        [HttpGet("pending")]
        public ActionResult GetPending([FromQuery] int page, [FromQuery] int size)
        {
            return Ok(giftService.GetPending(page, size));
        }

        [HttpGet("completed")]
        public ActionResult GetCompleted([FromQuery] int page, [FromQuery] int size)
        {
            return Ok(giftService.GetCompleted(page, size));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(long id)
        {
            return Ok(giftService.GetById(id));
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
