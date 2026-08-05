using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;
using System.Diagnostics.Eventing.Reader;
using System.Dynamic;
using System.Net;

namespace BlizuTebe.Services
{
    public class HelpRequestService : IHelpRequestService
    {
        private readonly IMapper _mapper;
        private readonly IHelpRequestRepository helpRequestRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUserRepository userRepository;

        public HelpRequestService(IMapper mapper, IHelpRequestRepository helpRequestRepository, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository)
        {
            _mapper = mapper;
            this.helpRequestRepository = helpRequestRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.userRepository = userRepository;
        }

        public Result<HelpRequestDto> Create(HelpRequestUpdateDto dto)
        {
            var newHelpRequest = _mapper.Map<HelpRequest>(dto);

            newHelpRequest.PostDate = DateTime.UtcNow;
            newHelpRequest.ExpireDate = newHelpRequest.PostDate.AddMonths(1);
            newHelpRequest.Status = HelpStatus.Pending;

            if (dto.Attachment != null && dto.Attachment.Length > 0)
            {
                newHelpRequest.Attachment = SaveImage(dto.Attachment);
            }

            helpRequestRepository.Create(newHelpRequest);
            return Result.Ok(_mapper.Map<HelpRequestDto>(newHelpRequest));

        }

        public Result<HelpRequestDto> Update(HelpRequestUpdateDto dto)
        {
            var helpRequest = helpRequestRepository.GetById(dto.Id);
            if (helpRequest == null)
            {
                return Result.Fail<HelpRequestDto>("Help Request not found with ID: " + dto.Id);
            }
            _mapper.Map(dto, helpRequest);
            if(dto.Attachment != null && dto.Attachment.Length > 0)
            {
                if (!string.IsNullOrEmpty(helpRequest.Attachment))
                {
                    DeleteImage(helpRequest.Attachment);
                }
                helpRequest.Attachment = SaveImage(dto.Attachment);
            }
            helpRequest.Status = dto.Status;
            helpRequestRepository.Update(helpRequest);
            return Result.Ok(_mapper.Map<HelpRequestDto>(helpRequest));
        }

        public Result<HelpRequestDto> Delete(long id)
        {
            var helpRequest = helpRequestRepository.GetById(id);
            if(helpRequest == null)
            {
                return Result.Fail<HelpRequestDto>("Help request not found with id: " + id);
            }
            //TODO: ovde je opet onaj cudan deo delete image, proveri sta to znaci
            if (!string.IsNullOrEmpty(helpRequest.Attachment))
            {
                DeleteImage(helpRequest.Attachment);
            }

            helpRequestRepository.Delete(id);
            return Result.Ok(_mapper.Map<HelpRequestDto>(helpRequest));
        }

        private List<HelpRequest> GetAllInternal(HelpType type)
        {
            var requests = helpRequestRepository.GetAll(type);
            UpdateExpiredRequests(requests);
            return requests;
        }

        public Result<List<HelpRequestDto>> GetAll(HelpType helpType)
        {
            var requests = GetAllInternal(helpType);
            return Result.Ok(_mapper.Map<List<HelpRequestDto>>(requests));
        }

        public Result<HelpRequestDto> GetById(long id)
        {
            var helpRequest = helpRequestRepository.GetById(id);
            if (helpRequest == null)
            {
                return Result.Fail("Not Found");
            }

            else{
                if(helpRequest.ExpireDate < DateTime.UtcNow && helpRequest.Status == HelpStatus.Pending)
                {
                    helpRequest.Status = HelpStatus.Expired;
                    helpRequestRepository.Update(helpRequest);
                }
            }
            return Result.Ok(_mapper.Map<HelpRequestDto>(helpRequest));
        }

        private List<HelpRequestDto> GetFilteredInternal(HelpType type, HelpStatus status)
        {
            var all = GetAllInternal(type);

            var result = all
                .Where(x => x.Status == status)
                .ToList();

            return _mapper.Map<List<HelpRequestDto>>(result);
        }

        public Result<List<HelpRequestDto>> GetPending(HelpType helpType)
        {
            return Result.Ok(GetFilteredInternal(helpType, HelpStatus.Pending));
        }

        public Result<List<HelpRequestDto>> GetCompleted(HelpType helpType)
        {
            return Result.Ok(GetFilteredInternal(helpType, HelpStatus.Completed));
        }

        public Result<List<HelpRequestDto>> GetByCategory(HelpType helpType, HelpCategory helpCategory)
        {
            var res = helpRequestRepository.GetByCategory(helpType, helpCategory).ToList();
            return Result.Ok(_mapper.Map<List<HelpRequestDto>>(res));
        }

        public Result<List<HelpRequestDto>> GetMyExpired(HelpType helpType, long id)
        {
            var res = GetFilteredInternal(helpType, HelpStatus.Expired).Where(x => x.UserId == id).ToList();
            return Result.Ok(res);
        }

        private string SaveImage(IFormFile file)
        {
            var folder = Path.Combine(webHostEnvironment.WebRootPath, "images", "helpRequests");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
            var path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        private void DeleteImage(string fileName)
        {
            var path = Path.Combine(webHostEnvironment.WebRootPath, "images", "helpRequests", fileName);
            
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void UpdateExpiredRequests(List<HelpRequest> requests)
        {
            foreach (var hr in requests)
            {
                if (hr.ExpireDate < DateTime.UtcNow && hr.Status == HelpStatus.Pending)
                {
                    hr.Status = HelpStatus.Expired;
                    helpRequestRepository.Update(hr);
                }
            }
        }
    }
}
