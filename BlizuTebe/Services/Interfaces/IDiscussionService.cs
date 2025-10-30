using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IDiscussionService
    {
        Result<DiscussionDto> Create(DiscussionDto dto);
        Result<DiscussionDto> UpdateById(long id, DiscussionDto dto);
        Result<DiscussionDto> DeleteById(long id);
        Result<List<DiscussionDto>> GetAll();
        Result<DiscussionDto> GetById(long id);
        Result<List<DiscussionDto>> GetAllSorted();
    }
}
