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
        private readonly IGiftService _giftService;
        private readonly IHelpRequestService _helpRequestService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public ReportService(IMapper mapper, IReportRepository repository, IGiftService giftService, IHelpRequestService helpRequestService, INotificationService notificationService, IUserService userService)
        {
            _mapper = mapper;
            _repository = repository;
            _giftService = giftService;
            _helpRequestService = helpRequestService;
            _notificationService = notificationService;
            _userService = userService;
        }

        public Result<ReportDto> Create(ReportDto reportDto)
        {
            var newReport = _mapper.Map<Report>(reportDto);
            newReport.Timestamp = DateTime.UtcNow;

            var admin = _userService.GetAdminForUser(reportDto.ReporterId);
            if (admin.IsFailed)
                return Result.Fail<ReportDto>(admin.Errors);

            RelatedObjectType relatedObjectType;
            if (reportDto.PostType == PostType.HelpRequest)
                relatedObjectType = RelatedObjectType.HelpRequest;
            else if (reportDto.PostType == PostType.Gift)
                relatedObjectType = RelatedObjectType.Gift;
            else
                return Result.Fail<ReportDto>("Invalid post type.");

            _repository.Create(newReport);

            var newNotification = new Notification("Imate novu prijavu", NotificationType.NewReport, admin.Value.Id, DateTime.UtcNow, newReport.Id, relatedObjectType);

            _notificationService.Create(
                _mapper.Map<NotificationDto>(newNotification)
            );

            return Result.Ok(_mapper.Map<ReportDto>(newReport));
        }

        public Result<ReportDto> Update(ReportUpdateDto reportUpdateDto)
        {
            var report = _repository.GetById(reportUpdateDto.Id);

            if (report == null)
                return Result.Fail<ReportDto>("Report not found with ID: " + reportUpdateDto.Id);

            report.Status = reportUpdateDto.Status;
            var reporter = _userService.GetByIdInternal(report.ReporterId);

            RelatedObjectType relatedObjectType = RelatedObjectType.HelpRequest;
            if (report.PostType == PostType.HelpRequest)
                relatedObjectType = RelatedObjectType.HelpRequest;
            else if (report.PostType == PostType.Gift)
                relatedObjectType = RelatedObjectType.Gift;

            if (reporter.IsFailed)
                return Result.Fail<ReportDto>(reporter.Errors);

            if (reportUpdateDto.Status == ReportStatus.Accepted)
            {
                long ownerId;
                if (report.PostType == PostType.Gift)
                {
                    var gift = _giftService.GetById(report.PostId);
                    if (gift.IsFailed)
                        return Result.Fail<ReportDto>(gift.Errors);
                    ownerId = gift.Value.UserId;
                    _giftService.Delete(report.PostId);
                }
                else if (report.PostType == PostType.HelpRequest)
                {
                    var helpRequest = _helpRequestService.GetById(report.PostId);
                    if (helpRequest.IsFailed)
                        return Result.Fail<ReportDto>(helpRequest.Errors);
                    ownerId = helpRequest.Value.UserId;
                    _helpRequestService.Delete(report.PostId);
                }
                else
                    return Result.Fail<ReportDto>("Invalid post type.");

                var ownerNotification = new Notification("Prijava za Vašu objavu je prihvaćena, a objava obrisana", NotificationType.ReportAccepted, ownerId, DateTime.UtcNow, report.Id, relatedObjectType);
                var reporterNotification = new Notification("Vaša prijava je prihvaćena", NotificationType.ReportAccepted, report.ReporterId, DateTime.UtcNow, report.Id, relatedObjectType);

                _notificationService.Create(_mapper.Map<NotificationDto>(ownerNotification));
                _notificationService.Create(_mapper.Map<NotificationDto>(reporterNotification));
            }
            else if (reportUpdateDto.Status == ReportStatus.Rejected)
            {
                var reporterNotification = new Notification("Vaša prijava je odbijena", NotificationType.ReportRejected, report.ReporterId, DateTime.UtcNow, report.Id, relatedObjectType);
                _notificationService.Create(_mapper.Map<NotificationDto>(reporterNotification));
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
