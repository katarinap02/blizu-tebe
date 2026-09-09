using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPost]
        public IActionResult Create([FromBody] ChatDto chatDto)
        {
            var result = _chatService.Create(chatDto);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPut]
        public IActionResult Update([FromBody] ChatDto chatDto)
        {
            var result = _chatService.Update(chatDto);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _chatService.Delete(id);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("user/{userId}")]
        public IActionResult GetAllForUser(long userId)
        {
            var result = _chatService.GetAllForUser(userId);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }


        [Authorize(Roles = "Admin,Member")]
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _chatService.GetById(id);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("users")]
        public IActionResult GetByUsers([FromQuery] long user1Id, [FromQuery] long user2Id, [FromQuery] long postId, [FromQuery] PostType postType)
        {
            var result = _chatService.GetByUsers( user1Id, user2Id, postId, postType);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }


        [Authorize(Roles = "Admin,Member")]
        [HttpPost("get-or-create")]
        public IActionResult GetOrCreate([FromQuery] long user1Id, [FromQuery] long user2Id, [FromQuery] long postId, [FromQuery] PostType postType)
        {
            var result = _chatService.GetOrCreate(
                user1Id, user2Id, postId, postType);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
        
    }
}
