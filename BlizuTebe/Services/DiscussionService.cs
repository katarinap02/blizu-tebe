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
        private readonly IDiscussionRepository _discussionRepository;
        private readonly IDiscussionCommentRepository _discussionCommentRepository;

        public DiscussionService(IMapper mapper, IDiscussionRepository discussionRepository, IDiscussionCommentRepository discussionCommentRepository)
        {
            _mapper = mapper;
            _discussionRepository = discussionRepository;
            _discussionCommentRepository = discussionCommentRepository;
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
            var comments = _discussionCommentRepository.GetByDiscussionId(id);

            foreach (var comment in comments)
            {
                _discussionCommentRepository.Delete(comment);
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

        public Result<List<DiscussionDto>> GetAllSorted()
        {
            var discussions = _discussionRepository.GetAll();
            var comments = _discussionCommentRepository.GetAll(); 

            var now = DateTime.UtcNow;

            var sorted = discussions
                .Select(d =>
                {
                    var discussionComments = comments.Where(c => c.DiscussionId == d.Id).ToList();

                    int commentCount = discussionComments.Count;
                    DateTime lastActivity = discussionComments.Any()
                        ? discussionComments.Max(c => c.CommentedAt)
                        : d.CreatedAt;

                    // Formula za score
                    double score =
                        (d.isPinned ? 10000 : 0) +                // Pinned je najvazniji da ide na vrh
                        (commentCount * 1.5) -                    // Koliko ima komentara u chat-u
                        ((now - lastActivity).TotalHours * 0.1);  // Kad je poslednja poruka poslata

                    return new { Discussion = d, Score = score };
                })
                .OrderByDescending(x => x.Score)
                .Select(x => x.Discussion)
                .ToList();

            return Result.Ok(_mapper.Map<List<DiscussionDto>>(sorted));
        }

    }
}
