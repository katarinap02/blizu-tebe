using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _context;

        public RatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Rating rating)
        {
            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }

        public void Delete(Rating rating)
        {
            _context.Ratings.Remove(rating);
            _context.SaveChanges();
        }

        public List<Rating> GetAll()
        {
            return _context.Ratings.ToList();
        }

        public Rating? GetById(long id)
        {
            return _context.Ratings.FirstOrDefault(r => r.Id == id);
        }

        public List<Rating> GetByRatedId(long ratedId)
        {
            return _context.Ratings.Where(r => r.RatedId == ratedId).ToList();
        }

        public List<Rating> GetByRaterId(long raterId)
        {
            return _context.Ratings.Where(r => r.RaterId == raterId).ToList();
        }

        public void Update(Rating rating)
        {
            _context.Ratings.Update(rating);
            _context.SaveChanges();
        }
    }
}
