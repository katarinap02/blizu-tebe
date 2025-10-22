using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface ICommunityRequestService
    {
        Result<CommunityRequestDto> Create(CommunityRequestDto dto);
        Result<CommunityRequestDto> UpdateById(long id, CommunityRequestDto dto);
        Result<CommunityRequestDto> DeleteById(long id);
        Result<List<CommunityRequestDto>> GetAll();
        Result<CommunityRequestDto> GetById(long id);
    }
}
