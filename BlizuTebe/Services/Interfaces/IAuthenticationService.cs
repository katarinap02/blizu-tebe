
using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Result<AuthenticationTokensDto> Login(CredentialsDto credentials);
    }
}
