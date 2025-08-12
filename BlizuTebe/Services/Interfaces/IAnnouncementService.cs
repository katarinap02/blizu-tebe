using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IAnnouncementService
    {
        Result<AnnouncementDto> Create(AnnouncementDto dto);
        Result<AnnouncementDto> getById(long id);

        Result<AnnouncementDto> deleteById(long id);

        Result<AnnouncementDto> updateById(long id, AnnouncementDto dto);

        Result<List<AnnouncementDto>> getAnnouncements();
    }
}
