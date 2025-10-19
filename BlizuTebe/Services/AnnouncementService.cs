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
        private readonly IWebHostEnvironment _environment;

        public AnnouncementService(IMapper mapper, IAnnouncementRepository announcementRepository, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _announcementRepository = announcementRepository;
            _environment = environment;
        }

        public Result<AnnouncementDto> Create(AnnouncementDto dto)
        {
            var newAnnouncement = _mapper.Map<Announcement>(dto);
            if (newAnnouncement == null)
            {
                return Result.Fail<AnnouncementDto>("Announcement not found.");
            }

            if (dto.Picture != null && dto.Picture.Length > 0)
            {
                newAnnouncement.Picture = SaveImage(dto.Picture);
            }
            newAnnouncement.PublishedAt = DateTime.SpecifyKind(newAnnouncement.PublishedAt, DateTimeKind.Utc);
            newAnnouncement.ExpirationDate = DateTime.SpecifyKind(newAnnouncement.ExpirationDate, DateTimeKind.Utc);

            _announcementRepository.Create(newAnnouncement);
            return Result.Ok(_mapper.Map<AnnouncementDto>(newAnnouncement));
        }

        public Result<AnnouncementDto> UpdateById(long id, AnnouncementDto dto)
        {
            var announcementToUpdate = _announcementRepository.GetById(id);
            if (announcementToUpdate == null)
            {
                return Result.Fail<AnnouncementDto>("Announcement not found with ID: " + id);
            }

            _mapper.Map(dto, announcementToUpdate);
            announcementToUpdate.PublishedAt = DateTime.SpecifyKind(announcementToUpdate.PublishedAt, DateTimeKind.Utc);
            announcementToUpdate.ExpirationDate = DateTime.SpecifyKind(announcementToUpdate.ExpirationDate, DateTimeKind.Utc);

            if (dto.Picture != null && dto.Picture.Length > 0)
            {
                if (!string.IsNullOrEmpty(announcementToUpdate.Picture))
                {
                    DeleteImage(announcementToUpdate.Picture);
                }

                announcementToUpdate.Picture = SaveImage(dto.Picture);
            }
            else if (!string.IsNullOrEmpty(dto.ExistingPicture))
            {
                announcementToUpdate.Picture = dto.ExistingPicture;
            }

            _announcementRepository.Update(announcementToUpdate);
            return Result.Ok(_mapper.Map<AnnouncementDto>(announcementToUpdate));
        }


        public Result<AnnouncementDto> DeleteById(long id)
        {
            var announcement = _announcementRepository.GetById(id);
            if (announcement == null)
            {
                return Result.Fail<AnnouncementDto>("Announcement not found with ID: " + id);
            }
            if (!string.IsNullOrEmpty(announcement.Picture))
            {
                DeleteImage(announcement.Picture);
            }

            _announcementRepository.Delete(announcement);
            return Result.Ok(_mapper.Map<AnnouncementDto>(announcement));
        }

        public Result<List<AnnouncementDto>> GetAnnouncements()
        {
            var announcements = _announcementRepository.GetAll();
            return Result.Ok(_mapper.Map<List<AnnouncementDto>>(announcements));
        }

        public Result<AnnouncementDto> GetById(long id)
        {
            var an = _announcementRepository.GetById(id);
            if (an == null)
            {
                return Result.Fail("Not found");
            }
            else return Result.Ok(_mapper.Map<AnnouncementDto>(an));
        }

        private string SaveImage(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "announcements");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        private void DeleteImage(string fileName)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "images", "announcements", fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


    }
}
