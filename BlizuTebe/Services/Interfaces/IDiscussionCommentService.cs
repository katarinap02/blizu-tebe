using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IDiscussionCommentService
    {
        Result<DiscussionCommentDto> Create(DiscussionCommentDto dto);
        Result<DiscussionCommentDto> UpdateById(long id, DiscussionCommentDto dto);
        Result<DiscussionCommentDto> DeleteById(long id);
        Result<List<DiscussionCommentDto>> GetAll();
        Result<DiscussionCommentDto> GetById(long id);
        Result<List<DiscussionCommentDto>> GetByDiscussionId(long discussionId);
    }
}
