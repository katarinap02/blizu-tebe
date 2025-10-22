using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class RatingService : IRatingService
    {
        private readonly IMapper _mapper;
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IMapper mapper, IRatingRepository ratingRepository)
        {
            _mapper = mapper;
            _ratingRepository = ratingRepository;
        }

        public Result<RatingDto> Create(RatingDto dto)
        {
            var newRating = _mapper.Map<Rating>(dto);
            if (newRating == null)
            {
                return Result.Fail<RatingDto>("Rating not found.");
            }

            newRating.TimeStamp = DateTime.SpecifyKind(newRating.TimeStamp, DateTimeKind.Utc);

            _ratingRepository.Create(newRating);
            return Result.Ok(_mapper.Map<RatingDto>(newRating));
        }

        public Result<RatingDto> UpdateById(long id, RatingDto dto)
        {
            var ratingToUpdate = _ratingRepository.GetById(id);
            if (ratingToUpdate == null)
            {
                return Result.Fail<RatingDto>("Rating not found with ID: " + id);
            }

            _mapper.Map(dto, ratingToUpdate);
            ratingToUpdate.TimeStamp = DateTime.SpecifyKind(ratingToUpdate.TimeStamp, DateTimeKind.Utc);

            _ratingRepository.Update(ratingToUpdate);
            return Result.Ok(_mapper.Map<RatingDto>(ratingToUpdate));
        }

        public Result<RatingDto> DeleteById(long id)
        {
            var rating = _ratingRepository.GetById(id);
            if (rating == null)
            {
                return Result.Fail<RatingDto>("Rating not found with ID: " + id);
            }

            _ratingRepository.Delete(rating);
            return Result.Ok(_mapper.Map<RatingDto>(rating));
        }

        public Result<List<RatingDto>> GetAll()
        {
            var ratings = _ratingRepository.GetAll();
            return Result.Ok(_mapper.Map<List<RatingDto>>(ratings));
        }

        public Result<RatingDto> GetById(long id)
        {
            var rating = _ratingRepository.GetById(id);
            if (rating == null)
            {
                return Result.Fail("Rating not found");
            }
            return Result.Ok(_mapper.Map<RatingDto>(rating));
        }

        public Result<List<RatingDto>> GetByRaterId(long raterId)
        {
            var ratings = _ratingRepository.GetByRaterId(raterId);
            return Result.Ok(_mapper.Map<List<RatingDto>>(ratings));
        }

        public Result<List<RatingDto>> GetByRatedId(long ratedId)
        {
            var ratings = _ratingRepository.GetByRatedId(ratedId);
            return Result.Ok(_mapper.Map<List<RatingDto>>(ratings));
        }
    }
}
