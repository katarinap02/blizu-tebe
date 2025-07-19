using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public Result<UserDto> getById(long id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return Result.Fail("Not found");
            }
            else return Result.Ok(_mapper.Map<UserDto>(user));
        }
    }
}
