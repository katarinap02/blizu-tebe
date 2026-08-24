using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class ReportRepository : IReportRepository
    {

        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Report report)
        {
            _context.Report.Add(report);
            _context.SaveChanges();
        }

        public void Update(Report report)
        {
            _context.Report.Update(report);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entity = _context.Report.Find(id);
            if (entity == null) return;
            _context.Report.Remove(entity);
            _context.SaveChanges();
        }

        public Report GetById(long id)
        {
            return _context.Report.Find(id);
        }

        public PagedResult<Report> GetAllPending(int page, int size)
        {
            var query = _context.Report.AsQueryable().Where(x => x.Status == ReportStatus.Pending);
            var totalCount = query.Count();
            var items = query.OrderByDescending(x => x.Timestamp)
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            return new PagedResult<Report>(items, totalCount);
        }
    }
}
