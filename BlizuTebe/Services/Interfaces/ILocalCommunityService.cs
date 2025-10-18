using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface ILocalCommunityService
    {
        Result<LocalCommunityDto> Create(LocalCommunityDto dto);
        Result<LocalCommunityDto> GetById(long id);
        Result<List<LocalCommunityDto>> GetAll();
        Result<LocalCommunityDto> Delete(long id);
        Result<LocalCommunityDto> GetByLocation(double latitude, double longitude);
    }
}
