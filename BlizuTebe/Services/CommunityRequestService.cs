using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class CommunityRequestService : ICommunityRequestService
    {
        private readonly IMapper _mapper;
        private readonly ICommunityRequestRepository _communityRequestRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly ICommunityRequestUsersRepository _communityUsersRepository;

        public CommunityRequestService(IMapper mapper, ICommunityRequestRepository communityRequestRepository, IWebHostEnvironment environment, ICommunityRequestUsersRepository usersRepository)
        {
            _mapper = mapper;
            _communityRequestRepository = communityRequestRepository;
            _environment = environment;
            _communityUsersRepository = usersRepository;
        }

        public Result<CommunityRequestDto> Create(CommunityRequestDto dto)
        {
            var newRequest = _mapper.Map<CommunityRequest>(dto);
            if (newRequest == null)
            {
                return Result.Fail<CommunityRequestDto>("Community request not found.");
            }

            if (dto.FilePicture != null && dto.FilePicture.Length > 0)
            {
                newRequest.Picture = SaveImage(dto.FilePicture);
            }
            newRequest.CreatedAt = DateTime.SpecifyKind(newRequest.CreatedAt, DateTimeKind.Utc);

            _communityRequestRepository.Create(newRequest);
            return Result.Ok(_mapper.Map<CommunityRequestDto>(newRequest));
        }

        public Result<CommunityRequestDto> UpdateById(long id, CommunityRequestDto dto)
        {
            var requestToUpdate = _communityRequestRepository.GetById(id);
            if (requestToUpdate == null)
            {
                return Result.Fail<CommunityRequestDto>("Community request not found with ID: " + id);
            }

            _mapper.Map(dto, requestToUpdate);
            requestToUpdate.CreatedAt = DateTime.SpecifyKind(requestToUpdate.CreatedAt, DateTimeKind.Utc);

            if (dto.FilePicture != null && dto.FilePicture.Length > 0)
            {
                if (!string.IsNullOrEmpty(requestToUpdate.Picture))
                {
                    DeleteImage(requestToUpdate.Picture);
                }

                requestToUpdate.Picture = SaveImage(dto.FilePicture);
            }
            else if (!string.IsNullOrEmpty(dto.Picture))
            {
                requestToUpdate.Picture = dto.Picture;
            }

            _communityRequestRepository.Update(requestToUpdate);
            return Result.Ok(_mapper.Map<CommunityRequestDto>(requestToUpdate));
        }

        public Result<CommunityRequestDto> DeleteById(long id)
        {
            var request = _communityRequestRepository.GetById(id);
            if (request == null)
            {
                return Result.Fail<CommunityRequestDto>("Community request not found with ID: " + id);
            }
            if (!string.IsNullOrEmpty(request.Picture))
            {
                DeleteImage(request.Picture);
            }

            var relatedUsers = _communityUsersRepository.GetByRequestId(id);

            foreach (var userLink in relatedUsers)
            {
                _communityUsersRepository.Delete(userLink);
            }

            _communityRequestRepository.Delete(request);

            return Result.Ok(_mapper.Map<CommunityRequestDto>(request));
        }

        public Result<List<CommunityRequestDto>> GetAll()
        {
            var requests = _communityRequestRepository.GetAll();
            return Result.Ok(_mapper.Map<List<CommunityRequestDto>>(requests));
        }

        public Result<CommunityRequestDto> GetById(long id)
        {
            var request = _communityRequestRepository.GetById(id);
            if (request == null)
            {
                return Result.Fail("Community request not found");
            }
            return Result.Ok(_mapper.Map<CommunityRequestDto>(request));
        }

        private string SaveImage(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "community_requests");

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
            var filePath = Path.Combine(_environment.WebRootPath, "images", "community_requests", fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

}
