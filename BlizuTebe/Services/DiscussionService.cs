using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IMapper _mapper;
        private readonly IDiscussionRepostiry _discussionRepository;

        public DiscussionService(IMapper mapper, DiscussionRepository discussionRepository)
        {
            _mapper = mapper;
            _discussionRepository = discussionRepository;
        }

        public Result<DiscussionDto> Create(DiscussionDto dto)
        {
            var newDiscussion = _mapper.Map<Discussion>(dto);
            if (newDiscussion == null)
            {
                return Result.Fail<DiscussionDto>("Discussion not found.");
            }

            newDiscussion.CreatedAt = DateTime.SpecifyKind(newDiscussion.CreatedAt, DateTimeKind.Utc);

            _discussionRepository.Create(newDiscussion);
            return Result.Ok(_mapper.Map<DiscussionDto>(newDiscussion));
        }

        public Result<DiscussionDto> UpdateById(long id, DiscussionDto dto)
        {
            var discussionToUpdate = _discussionRepository.GetById(id);
            if (discussionToUpdate == null)
            {
                return Result.Fail<DiscussionDto>("Discussion not found with ID: " + id);
            }

            _mapper.Map(dto, discussionToUpdate);
            discussionToUpdate.CreatedAt = DateTime.SpecifyKind(discussionToUpdate.CreatedAt, DateTimeKind.Utc);

            _discussionRepository.Update(discussionToUpdate);
            return Result.Ok(_mapper.Map<DiscussionDto>(discussionToUpdate));
        }

        public Result<DiscussionDto> DeleteById(long id)
        {
            var discussion = _discussionRepository.GetById(id);
            if (discussion == null)
            {
                return Result.Fail<DiscussionDto>("Discussion not found with ID: " + id);
            }

            _discussionRepository.Delete(discussion);
            return Result.Ok(_mapper.Map<DiscussionDto>(discussion));
        }

        public Result<List<DiscussionDto>> GetAll()
        {
            var discussions = _discussionRepository.GetAll();
            return Result.Ok(_mapper.Map<List<DiscussionDto>>(discussions));
        }

        public Result<DiscussionDto> GetById(long id)
        {
            var discussion = _discussionRepository.GetById(id);
            if (discussion == null)
            {
                return Result.Fail("Discussion not found");
            }
            return Result.Ok(_mapper.Map<DiscussionDto>(discussion));
        }
    }
}
