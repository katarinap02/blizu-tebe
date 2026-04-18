using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;
using System.Diagnostics.Eventing.Reader;
using System.Dynamic;

namespace BlizuTebe.Services
{
    public class HelpRequestService : IHelpRequestService
    {
        private readonly IMapper _mapper;
        private readonly IHelpRequestRepository helpRequestRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HelpRequestService(IMapper mapper, IHelpRequestRepository helpRequestRepository, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            this.helpRequestRepository = helpRequestRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public Result<HelpRequestDto> Create(HelpRequestDto dto)
        {
            var newHelpRequest = _mapper.Map<HelpRequest>(dto);

            newHelpRequest.PostDate = DateTime.UtcNow;
            newHelpRequest.ExpireDate = newHelpRequest.PostDate.AddMonths(1);
            newHelpRequest.Status = HelpStatus.Pending;

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
            /*if(dto.Attachment != null && dto.Attachment.Length > 0)
            {
                if (!string.IsNullOrEmpty(helpRequest.Attachment))
                {
                    DeleteImage(helpRequest.Attachment);
                }
                helpRequest.Attachment = SaveImage(dto.Attachment);
            }*/
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
            helpRequestRepository.Delete(id);
            return Result.Ok(_mapper.Map<HelpRequestDto>(helpRequest));
        }

        public Result<List<HelpRequestDto>> GetAll()
        {
            var helpRequests = helpRequestRepository.GetAll();
            foreach (var hr in helpRequests)
            {
                if (hr.ExpireDate < DateTime.UtcNow && hr.Status == HelpStatus.Pending)
                {
                    hr.Status = HelpStatus.Expired;
                    helpRequestRepository.Update(hr);
                }
            }
            return Result.Ok(_mapper.Map<List<HelpRequestDto>>(helpRequests));
        }

        public Result<HelpRequestDto> GetById(long id)
        {
            var helpRequest = helpRequestRepository.GetById(id);
            if (helpRequest == null)
            {
                return Result.Fail("Not Found");
            }
            else return Result.Ok(_mapper.Map<HelpRequestDto>(helpRequest));
        }

        private List<HelpRequestDto> GetFilteredInternal(HelpType type, HelpStatus status)
        {
            var result = helpRequestRepository.GetAll()
                .Where(x => x.HelpType == type && x.Status == status)
                .ToList();

            return _mapper.Map<List<HelpRequestDto>>(result);
        }

        public Result<List<HelpRequestDto>> GetPendingRequests()
        {
            return Result.Ok(GetFilteredInternal(HelpType.Asking, HelpStatus.Pending));
        }

        public Result<List<HelpRequestDto>> GetCompletedRequests()
        {
            return Result.Ok(GetFilteredInternal(HelpType.Asking, HelpStatus.Completed));
        }

        public Result<List<HelpRequestDto>> GetPendingOffers()
        {
            return Result.Ok(GetFilteredInternal(HelpType.Offering, HelpStatus.Pending));
        }

        public Result<List<HelpRequestDto>> GetCompletedOffers()
        {
            return Result.Ok(GetFilteredInternal(HelpType.Offering, HelpStatus.Completed));
        }
    }
}
