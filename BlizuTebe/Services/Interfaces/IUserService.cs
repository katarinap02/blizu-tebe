using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IUserService
    {
        Result<UserDto> Register(UserDto user);
        Result<UserViewDto> GetById(long id);
        Result<List<UserViewDto>> GetAll();
        Result<UserViewDto> DeleteById(long id);
        Result<UserViewDto> UpdateUser(long id, UserViewDto user);
        Result<UserViewDto> VerifyUser(long id);


    }
}
