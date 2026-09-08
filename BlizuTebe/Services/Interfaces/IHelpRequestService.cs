using BlizuTebe.Dtos;
using BlizuTebe.Models;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IHelpRequestService
    {
        Result<HelpRequestDto> Create(HelpRequestUpdateDto helpRequestDto);
        Result<HelpRequestDto> Update(HelpRequestUpdateDto helpRequestUpdateDto);
        Result<HelpRequestDto> Delete(long helpRequestId);
        Result<HelpRequestDto> GetById(long helpRequestId);
        Result<List<HelpRequestDto>> GetPending(HelpType helpType);
        Result<List<HelpRequestDto>> GetCompleted(HelpType helpType);
        Result<List<HelpRequestDto>> GetByCategory(HelpType helpType, HelpCategory helpCategory);
        Result<List<HelpRequestDto>> GetMyExpired(HelpType helpType, long id);
        Result<List<HelpRequestDto>> MatchRequestAndOffer(long helpId);
    }
}
