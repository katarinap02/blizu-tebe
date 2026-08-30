using BlizuTebe.Dtos;
using BlizuTebe.Services;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet]
        public IActionResult GetAllPending([FromQuery] int page, [FromQuery] int size)
        {
            var result = reportService.GetAllPending(page, size);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(long id)
        {
            var result = reportService.GetById(id);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        public ActionResult Create(ReportDto dto)
        {
            var result = reportService.Create(dto);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPut]
        public ActionResult Update(ReportUpdateDto dto)
        {
            return Ok(reportService.Update(dto));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            return Ok(reportService.Delete(id));
        }
    }
}
