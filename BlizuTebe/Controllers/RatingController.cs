using BlizuTebe.Dtos;
using BlizuTebe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlizuTebe.Controllers
{
    [Route("api/rating")]
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPost]
        public ActionResult<RatingDto> CreateRating([FromBody] RatingDto dto)
        {
            var result = _ratingService.Create(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPut("{id}")]
        public ActionResult<RatingDto> UpdateRating([FromRoute] long id, [FromBody] RatingDto dto)
        {
            var result = _ratingService.UpdateById(id, dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet]
        public ActionResult<List<RatingDto>> GetAllRatings()
        {
            var result = _ratingService.GetAll();
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<RatingDto> DeleteRating([FromRoute] long id)
        {
            var result = _ratingService.DeleteById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getById/{id}")]
        public ActionResult<RatingDto> GetRatingById([FromRoute] long id)
        {
            var result = _ratingService.GetById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getByRaterId/{raterId}")]
        public ActionResult<List<RatingDto>> GetByRaterId([FromRoute] long raterId)
        {
            var result = _ratingService.GetByRaterId(raterId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getByRatedId/{ratedId}")]
        public ActionResult<List<RatingDto>> GetByRatedId([FromRoute] long ratedId)
        {
            var result = _ratingService.GetByRatedId(ratedId);
            return CreateResponse(result);
        }
    }
}
