using System.Security.Claims;
using Application.Dtos.Read;

namespace Application.Services;

public interface ITokenService
{
    string Generate(GenerateTokenParamsDto tokenParams);

    ClaimsPrincipal Validate(string token, string key);
}
