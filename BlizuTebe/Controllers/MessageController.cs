using BlizuTebe.Dtos;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    { 
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] MessageDto messageDto)
        {
            var result = _messageService.Create(messageDto);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("chat/{chatId}")]
        public IActionResult GetAllFromChat(long chatId)
        {
            var result = _messageService.GetAllFromChat(chatId);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
        
    }
}

