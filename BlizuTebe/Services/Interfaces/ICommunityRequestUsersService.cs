using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface ICommunityRequestUsersService
    {
        Result<CommunityRequestUsersDto> Create(CommunityRequestUsersDto dto);
        Result<CommunityRequestUsersDto> Delete(long userId, long requestId);
        Result<List<CommunityRequestUsersDto>> GetAll();
        Result<List<CommunityRequestUsersDto>> GetByUserId(long userId);
        Result<List<CommunityRequestUsersDto>> GetByRequestId(long requestId);
    }
}
