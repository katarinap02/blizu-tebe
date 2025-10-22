using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        void Create(Rating an);
        void Update(Rating an);
        void Delete(Rating an);
        Rating? GetById(long id);
        List<Rating> GetAll();
        List<Rating> GetByRaterId(long raterId);
        List<Rating> GetByRatedId(long ratedId);
    }
}
