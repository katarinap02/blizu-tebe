using BlizuTebe.Dtos;
using BlizuTebe.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlizuTebe.Authentication
{
    public class JwtGenerator : ITokenGenerator
    {
        private readonly string _key = Environment.GetEnvironmentVariable("JWT_KEY") ?? "this_is_a_very_long_secret_key_for_jwt_token_12345";
        private readonly string _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "blizu-tebe-api";
        private readonly string _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "blizu-tebe-frontend";

        public AuthenticationTokensDto GenerateAccessToken(User user)
        {
            var authenticationResponse = new AuthenticationTokensDto();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("id", user.Id.ToString()),
                new("username", user.Username),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            var jwt = CreateToken(claims, 60 * 24);
            authenticationResponse.Id = user.Id;
            authenticationResponse.AccessToken = jwt;

            return authenticationResponse;

        }

        private string CreateToken(IEnumerable<Claim> claims, double expirationTimeInMinutes)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddMinutes(expirationTimeInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
