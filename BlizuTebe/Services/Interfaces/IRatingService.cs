using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IRatingService
    {
        Result<RatingDto> Create(RatingDto dto);
        Result<RatingDto> UpdateById(long id, RatingDto dto);
        Result<RatingDto> DeleteById(long id);
        Result<List<RatingDto>> GetAll();
        Result<RatingDto> GetById(long id);
        Result<List<RatingDto>> GetByRaterId(long raterId);
        Result<List<RatingDto>> GetByRatedId(long ratedId);
    }
}
