using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class DiscussionCommentService : IDiscussionCommentService
    {
        private readonly IMapper _mapper;
        private readonly IDiscussionCommentRepository _discussionCommentRepository;

        public DiscussionCommentService(IMapper mapper, IDiscussionCommentRepository discussionCommentRepository)
        {
            _mapper = mapper;
            _discussionCommentRepository = discussionCommentRepository;
        }

        public Result<DiscussionCommentDto> Create(DiscussionCommentDto dto)
        {
            var newComment = _mapper.Map<DiscussionComment>(dto);
            if (newComment == null)
            {
                return Result.Fail<DiscussionCommentDto>("Discussion comment not found.");
            }

            newComment.CommentedAt = DateTime.SpecifyKind(newComment.CommentedAt, DateTimeKind.Utc);

            _discussionCommentRepository.Create(newComment);
            return Result.Ok(_mapper.Map<DiscussionCommentDto>(newComment));
        }

        public Result<DiscussionCommentDto> UpdateById(long id, DiscussionCommentDto dto)
        {
            var commentToUpdate = _discussionCommentRepository.GetById(id);
            if (commentToUpdate == null)
            {
                return Result.Fail<DiscussionCommentDto>("Discussion comment not found with ID: " + id);
            }

            _mapper.Map(dto, commentToUpdate);
            commentToUpdate.CommentedAt = DateTime.SpecifyKind(commentToUpdate.CommentedAt, DateTimeKind.Utc);

            _discussionCommentRepository.Update(commentToUpdate);
            return Result.Ok(_mapper.Map<DiscussionCommentDto>(commentToUpdate));
        }

        public Result<DiscussionCommentDto> DeleteById(long id)
        {
            var comment = _discussionCommentRepository.GetById(id);
            if (comment == null)
            {
                return Result.Fail<DiscussionCommentDto>("Discussion comment not found with ID: " + id);
            }

            _discussionCommentRepository.Delete(comment);
            return Result.Ok(_mapper.Map<DiscussionCommentDto>(comment));
        }

        public Result<List<DiscussionCommentDto>> GetAll()
        {
            var comments = _discussionCommentRepository.GetAll();
            return Result.Ok(_mapper.Map<List<DiscussionCommentDto>>(comments));
        }

        public Result<DiscussionCommentDto> GetById(long id)
        {
            var comment = _discussionCommentRepository.GetById(id);
            if (comment == null)
            {
                return Result.Fail("Discussion comment not found");
            }
            return Result.Ok(_mapper.Map<DiscussionCommentDto>(comment));
        }

        public Result<List<DiscussionCommentDto>> GetByDiscussionId(long discussionId)
        {
            var comments = _discussionCommentRepository.GetByDiscussionId(discussionId);
            return Result.Ok(_mapper.Map<List<DiscussionCommentDto>>(comments));
        }
    }
}