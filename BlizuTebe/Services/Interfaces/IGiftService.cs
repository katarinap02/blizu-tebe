using BlizuTebe.Dtos;
using BlizuTebe.Models;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IGiftService
    {
        Result<GiftDto> Create(GiftUpdateDto giftDto);
        Result<GiftDto> Update(GiftUpdateDto giftDto);
        Result<GiftDto> Delete(long giftId);
        Result<GiftDto> GetById(long giftId);
        Result<PagedResult<GiftDto>> GetAll(int page, int size, GiftCategory? category);
        Result<PagedResult<GiftDto>> GetPending(int page, int size);
        Result<PagedResult<GiftDto>> GetCompleted(int page, int size);
        //Result<PagedResult<GiftDto>> GetMyExpired(long id);
    }
}
