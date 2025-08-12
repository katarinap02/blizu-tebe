using BlizuTebe.Dtos;
using BlizuTebe.Services;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BlizuTebe.Controllers
{
    [Route("api/announcement")]
    public class AnnouncementContoller : BaseApiController
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementContoller(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<AnnouncementDto> CreateAnnouncement([FromBody] AnnouncementDto announcementDto)
        {
            // Pretpostavka je da servis ima 'create' metodu
            var result = _announcementService.Create(announcementDto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet]
        public ActionResult<List<AnnouncementDto>> GetAllAnnouncements()
        {
            var result = _announcementService.getAnnouncements();
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<AnnouncementDto> UpdateAnnouncement([FromRoute] long id, [FromBody] AnnouncementDto announcementDto)
        {
            var result = _announcementService.updateById(id, announcementDto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<AnnouncementDto> DeleteAnnouncement([FromRoute] long id)
        {
            var result = _announcementService.deleteById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getById/{id}")]
        public ActionResult<AnnouncementDto> geAnnouncementById([FromRoute] long id)
        {
            var an = _announcementService.getById(id);
            return CreateResponse(an);
        }
    }
}
