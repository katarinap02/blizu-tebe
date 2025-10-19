using BlizuTebe.Dtos;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BlizuTebe.Controllers
{
    [Route("api/localcommunity")]
    public class LocalCommunityController : BaseApiController
    {
        private readonly ILocalCommunityService _service;

        public LocalCommunityController(ILocalCommunityService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<LocalCommunityDto> Create([FromBody] LocalCommunityDto dto)
        {
            var result = _service.Create(dto);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<List<LocalCommunityDto>> GetAll()
        {
            var result = _service.GetAll();
            return CreateResponse(result);
        }

        [HttpGet("by-location")]
        public ActionResult<LocalCommunityDto> GetByLocation([FromQuery] double lat, [FromQuery] double lng)
        {
            var result = _service.GetByLocation(lat, lng);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("{id}")]
        public ActionResult<LocalCommunityDto> GetById([FromRoute] long id)
        {
            var result = _service.GetById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<LocalCommunityDto> Delete([FromRoute] long id)
        {
            var result = _service.Delete(id);
            return CreateResponse(result);
        }

    }
}
