using BlizuTebe.Dtos;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] NotificationDto notificationDto)
        {
            var result = notificationService.Create(notificationDto);
            if (result.IsFailed) return BadRequest(result.Errors);
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] NotificationUpdateDto dto)
        {
            var result = notificationService.Update(dto);
            if (result.IsFailed) return NotFound(result.Errors);
            return Ok(result.Value);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(long userId)
        {
            var result = notificationService.GetByUser(userId);
            if (result.IsFailed) return BadRequest(result.Errors);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = notificationService.GetById(id);
            if (result.IsFailed) return NotFound(result.Errors);
            return Ok(result.Value);
        }

    }
}
