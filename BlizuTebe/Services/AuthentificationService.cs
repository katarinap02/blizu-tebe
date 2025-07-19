using BlizuTebe.Authentication;
using BlizuTebe.Dtos;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class AuthentificationService : IAuthenticationService
    {

        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthentificationService(ITokenGenerator tokenGenerator, IUserRepository userRepository)
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
        }

        public Result<AuthenticationTokensDto> Login(CredentialsDto credentials)
        {
            var user = _userRepository.GetVerifiedByUsername(credentials.Username);
            if (user == null || credentials.Password != user.Password) return Result.Fail("NotFound");

            return _tokenGenerator.GenerateAccessToken(user);


        }
    }
}
