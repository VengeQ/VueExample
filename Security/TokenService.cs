using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Security
{
    public class TokenService : ITokenService
    {
        private readonly TokenOptions _tokenOptions;
        public TokenService(IOptions<TokenOptions> options) { _tokenOptions = options.Value; }

        public string GenerateToken(IDictionary<string, object> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwt = new SecurityTokenDescriptor()
            {
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience,
                Claims = claims,
                IssuedAt = null,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(_tokenOptions.ExpiresInMinutes),
                SigningCredentials = signingCredentials
            };

            var tokenString = new JsonWebTokenHandler().CreateToken(jwt);
            return tokenString;
        }
    }
}
