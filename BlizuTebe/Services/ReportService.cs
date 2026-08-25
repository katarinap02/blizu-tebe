using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _repository;
        private readonly IGiftRepository _giftRepository;
        private readonly IHelpRequestRepository _helpRequestRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUserRepository _userRepository;

        public ReportService(IMapper mapper, IReportRepository repository, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository, IGiftRepository giftRepository, IHelpRequestRepository helpRequestRepository)
        {
            _mapper = mapper;
            _repository = repository;
            this.webHostEnvironment = webHostEnvironment;
            _userRepository = userRepository;
            _giftRepository = giftRepository;
            _helpRequestRepository = helpRequestRepository;
        }

        public Result<ReportDto> Create(ReportDto reportDto)
        {
            var newReport = _mapper.Map<Report>(reportDto);

            newReport.Timestamp = DateTime.UtcNow;
            
            _repository.Create(newReport);
            return Result.Ok(_mapper.Map<ReportDto>(newReport));
        }

        public Result<ReportDto> Update(ReportUpdateDto reportUpdateDto)
        {
            var report = _repository.GetById(reportUpdateDto.Id);
            if (report == null) return Result.Fail<ReportDto>("Report not found with ID: " + reportUpdateDto.Id);

            report.Status = reportUpdateDto.Status;
            if (reportUpdateDto.Status == ReportStatus.Accepted)
            {
                if (report.PostType == PostType.Gift)
                {
                    _giftRepository.Delete(report.PostId);
                }
                else if (report.PostType == PostType.HelpRequest)
                {
                    _helpRequestRepository.Delete(report.PostId);
                }
            }
            _repository.Update(report);

            return Result.Ok(_mapper.Map<ReportDto>(report));
        }

        public Result<ReportDto> Delete(long id)
        {
            var report = _repository.GetById(id);
            if (report == null) return Result.Fail<ReportDto>("Report not found with ID: " + id);

            _repository.Delete(id);
            return Result.Ok(_mapper.Map<ReportDto>(report));
        }

        public Result<PagedResult<ReportDto>> GetAllPending(int page, int pageSize)
        {
            var reports = _repository.GetAllPending(page, pageSize);
            var result = new PagedResult<ReportDto>(_mapper.Map<List<ReportDto>>(reports.Results), reports.TotalCount);
            return result;
        }

        public Result<ReportDto> GetById(long id)
        {
            var report = _repository.GetById(id);
            if (report == null) 
                return Result.Fail("Not Found");

            return Result.Ok(_mapper.Map<ReportDto>(report));
        }
    }
}
