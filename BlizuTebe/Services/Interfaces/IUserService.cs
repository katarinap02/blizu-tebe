using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IUserService
    {

        Result<UserDto> getById(long id);
        Result<List<UserDto>> getAll();

        Result<UserDto> deleteById(long id);

        Result<UserDto> updateUser(long id, UserDto user);

    }
}
