using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface IReportRepository
    {
        void Create(Report report);
        Report? GetById(long id);
        PagedResult<Report> GetAllPending(int page, int size);
        void Update(Report report);
        void Delete(long id);
    }
}
