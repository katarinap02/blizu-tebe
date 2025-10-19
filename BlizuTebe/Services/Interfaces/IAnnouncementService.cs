using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IAnnouncementService
    {
        Result<AnnouncementDto> Create(AnnouncementDto dto);
        Result<AnnouncementDto> GetById(long id);

        Result<AnnouncementDto> DeleteById(long id);

        Result<AnnouncementDto> UpdateById(long id, AnnouncementDto dto);

        Result<List<AnnouncementDto>> GetAnnouncements();
    }
}
