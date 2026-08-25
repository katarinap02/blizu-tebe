using BlizuTebe.Dtos;
using BlizuTebe.Models;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IReportService
    {
        Result<ReportDto> Create(ReportDto reportDto);
        Result<ReportDto> Update(ReportUpdateDto reportDto);
        Result<ReportDto> Delete(long id);
        Result<ReportDto> GetById(long id);
        Result<PagedResult<ReportDto>> GetAllPending(int page, int size);
    }
}
