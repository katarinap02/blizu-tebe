using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class GiftRepository : IGiftRepository
    {
        private readonly AppDbContext _context;
        public GiftRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Gift gift)
        {
            _context.Gift.Add(gift);
            _context.SaveChanges();
        }

        public void Update(Gift gift)
        {
            _context.Gift.Update(gift);
            _context.SaveChanges();
        }

        public void Delete(long giftId)
        {
            var entity = _context.Gift.Find(giftId);
            if (entity == null) return;

            _context.Gift.Remove(entity);
            _context.SaveChanges();
        }

        public Gift GetById(long id)
        {
            return _context.Gift.Find(id);
        }

        public PagedResult<Gift> GetAll(int page, int size, GiftCategory? category, GiftStatus? status)
        {
            var query = _context.Gift.AsQueryable();

            if (category.HasValue)
            {
                query = query.Where(x => x.GiftCategory == category.Value);
            }

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            var totalCount = query.Count();
            var items = query.OrderByDescending(x => x.PostDate)
                                .Skip((page - 1) * size)
                                .Take(size)
                                .ToList();

            return new PagedResult<Gift>(items, totalCount);
        }


    }
}
