using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IMapper mapper, IAnnouncementRepository announcementRepository)
        {
            _mapper = mapper;
            _announcementRepository = announcementRepository;
        }

        public Result<AnnouncementDto> Create(AnnouncementDto dto)
        {
            var newAnnouncement = _mapper.Map<Announcement>(dto);
            if (newAnnouncement == null)
            {
                return Result.Fail<AnnouncementDto>("Announcement not found.");
            }

            _announcementRepository.Create(newAnnouncement);

            return Result.Ok(_mapper.Map<AnnouncementDto>(newAnnouncement));
        }

        

        public Result<AnnouncementDto> deleteById(long id)
        {
            var announcementToDelete = _announcementRepository.GetById(id);
            if (announcementToDelete == null)
            {
                return Result.Fail<AnnouncementDto>("Announcement not found with ID: " + id);
            }
            _announcementRepository.Delete(announcementToDelete);
            return Result.Ok(_mapper.Map<AnnouncementDto>(announcementToDelete));
        }

        public Result<List<AnnouncementDto>> getAnnouncements()
        {
            var announcements = _announcementRepository.GetAll();
            return Result.Ok(_mapper.Map<List<AnnouncementDto>>(announcements));
        }

        public Result<AnnouncementDto> getById(long id)
        {
            var an = _announcementRepository.GetById(id);
            if (an == null)
            {
                return Result.Fail("Not found");
            }
            else return Result.Ok(_mapper.Map<AnnouncementDto>(an));
        }

        public Result<AnnouncementDto> updateById(long id, AnnouncementDto dto)
        {
            var announcementToUpdate = _announcementRepository.GetById(id);
            if (announcementToUpdate == null)
            {
                return Result.Fail<AnnouncementDto>("Announcement not found with ID: " + id);
            }
            _mapper.Map(dto, announcementToUpdate);
            _announcementRepository.Update(announcementToUpdate);
            return Result.Ok(_mapper.Map<AnnouncementDto>(announcementToUpdate));
        }
    }
}
