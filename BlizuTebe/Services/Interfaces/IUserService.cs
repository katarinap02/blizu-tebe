using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IUserService
    {
        Result<UserDto> getById(long id);

    }
}
