using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class CommunityRequestUsersService : ICommunityRequestUsersService
    {
        private readonly IMapper _mapper;
        private readonly ICommunityRequestUsersRepository _communityRequestUsersRepository;

        public CommunityRequestUsersService(IMapper mapper, ICommunityRequestUsersRepository communityRequestUsersRepository)
        {
            _mapper = mapper;
            _communityRequestUsersRepository = communityRequestUsersRepository;
        }

        public Result<CommunityRequestUsersDto> Create(CommunityRequestUsersDto dto)
        {
            var existing = _communityRequestUsersRepository.GetByUserIdAndRequestId(dto.UserId, dto.CommunityRequestId);
            if (existing != null)
            {
                return Result.Fail<CommunityRequestUsersDto>("User is already associated with this community request.");
            }

            var newEntry = _mapper.Map<CommunityRequestUsers>(dto);
            if (newEntry == null)
            {
                return Result.Fail<CommunityRequestUsersDto>("Failed to map community request users.");
            }

            _communityRequestUsersRepository.Create(newEntry);
            return Result.Ok(_mapper.Map<CommunityRequestUsersDto>(newEntry));
        }

        public Result<CommunityRequestUsersDto> Delete(long userId, long requestId)
        {
            var entry = _communityRequestUsersRepository.GetByUserIdAndRequestId(userId, requestId);
            if (entry == null)
            {
                return Result.Fail<CommunityRequestUsersDto>("Association not found.");
            }

            _communityRequestUsersRepository.Delete(entry);
            return Result.Ok(_mapper.Map<CommunityRequestUsersDto>(entry));
        }

        public Result<List<CommunityRequestUsersDto>> GetAll()
        {
            var entries = _communityRequestUsersRepository.GetAll();
            return Result.Ok(_mapper.Map<List<CommunityRequestUsersDto>>(entries));
        }

        public Result<List<CommunityRequestUsersDto>> GetByUserId(long userId)
        {
            var entries = _communityRequestUsersRepository.GetByUserId(userId);
            return Result.Ok(_mapper.Map<List<CommunityRequestUsersDto>>(entries));
        }

        public Result<List<CommunityRequestUsersDto>> GetByRequestId(long requestId)
        {
            var entries = _communityRequestUsersRepository.GetByRequestId(requestId);
            return Result.Ok(_mapper.Map<List<CommunityRequestUsersDto>>(entries));
        }
    }

}
