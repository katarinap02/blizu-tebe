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
        private readonly IUserRepository _userRepository;
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;

        public RatingService(IMapper mapper, IRatingRepository ratingRepository, IUserRepository userRepository, IMessageService messageService, IChatService chatService)
        {
            _mapper = mapper;
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _messageService = messageService;
            _chatService = chatService;
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
            UpdateUserAverageRating(newRating.RatedId);
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

        public Result<bool> CanRateUser(long chatId)
        {
            var chat = _chatService.GetById(chatId);
            if (chat == null)
                return Result.Fail("Chat not found");

            var messages = _messageService.GetAllFromChat(chatId);
            if (messages.Value.Count < 3)
                return Result.Ok(false);

            bool user1Messaged = messages.Value.Any(m => m.SenderId == chat.Value.User1Id);
            bool user2Messaged = messages.Value.Any(m => m.SenderId == chat.Value.User2Id);

            if (user1Messaged && user2Messaged)
            {
                return Result.Ok(true);
            }

            return Result.Ok(false);
        }

        private void UpdateUserAverageRating(long ratedUserId)
        {
            var allRatings = _ratingRepository.GetByRatedId(ratedUserId);
            if (allRatings == null || allRatings.Count == 0)
                return;

            double average = allRatings.Average(r => r.Score);

            var user = _userRepository.GetById(ratedUserId);
            if (user != null)
            {
                user.Rating = Math.Round(average, 2);
                _userRepository.Update(user);
            }
        }
    }

}
