using BlizuTebe.Dtos;
using BlizuTebe.Models;

namespace BlizuTebe.Authentication
{
    public interface ITokenGenerator
    {
        AuthenticationTokensDto GenerateAccessToken(User user);
    }
}
