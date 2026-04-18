using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IHelpRequestService
    {
        Result<HelpRequestDto> Create(HelpRequestDto helpRequestDto);
        Result<HelpRequestDto> Update(HelpRequestUpdateDto helpRequestUpdateDto);
        Result<HelpRequestDto> Delete(long helpRequestId);
        Result<HelpRequestDto> GetById(long helpRequestId);
        Result<List<HelpRequestDto>> GetPendingRequests();
        Result<List<HelpRequestDto>> GetCompletedRequests();
        Result<List<HelpRequestDto>> GetPendingOffers();
        Result<List<HelpRequestDto>> GetCompletedOffers();

    }
}
