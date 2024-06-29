using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos.Read;
using Application.Services;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class TokenService : ITokenService
{
    public string Generate(GenerateTokenParamsDto tokenParams)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(tokenParams.Key);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = tokenParams.Claims,
            Expires = tokenParams.ExpiryDateTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(securityTokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    public ClaimsPrincipal Validate(string token, string key)
    {

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(key)
            ),
        };
        return tokenHandler.ValidateToken(token, validationParameters, out _);
    }
}
