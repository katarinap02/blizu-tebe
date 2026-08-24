using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface IGiftRepository
    {
        void Create(Gift gift);
        void Update(Gift gift);
        void Delete(long giftId);
        Gift? GetById(long id);
        PagedResult<Gift> GetAll(int page, int size, GiftCategory? category, GiftStatus? status);
        //PagedResult<Gift> GetMyExpired(int page, int size, long userId);
    }
}
