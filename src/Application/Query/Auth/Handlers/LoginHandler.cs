using System.Security.Claims;
using Application.Dtos.Read;
using Application.Services;
using Application.Utils;
using Application.Utils.Params;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Auth.Handlers;

public class LoginHandler : IRequestHandler<Login, JwtTokenDto>
{
    private readonly IUnitOfWork _uow;

    private readonly ITokenService _tokenService;

    public LoginHandler(IUnitOfWork uow, ITokenService tokenService)
    {
        _uow = uow;
        _tokenService = tokenService;
    }

    public async Task<JwtTokenDto> Handle(Login request, CancellationToken cancellationToken)
    {
        var employee = await _uow.Employees.GetByEmailAsync(request.Email);
        if (employee is null)
        {
            throw new WrongLoginCredentialsException();
        }

        var passwordParams = new PasswordParams
        {
            UnhashedPassword = request.Password,
        };

        if (!PasswordUtils.Verify(passwordParams, employee.Password, employee.Salt))
        {
            throw new WrongLoginCredentialsException();
        }

        var tokenParams = new GenerateTokenParamsDto
        {
            Claims = new ClaimsIdentity(
                new List<Claim> {
                    new Claim(ClaimTypes.Email, employee.Email),
                    new Claim(ClaimTypes.Name, employee.FirstName),
                    new Claim(ClaimTypes.Surname, employee.LastName),
                    new Claim(ClaimTypes.Role, employee.Role.ToString())
                }
            ),
            Key = request.Key,
            ExpiryDateTime = DateTime.Now.AddMinutes(Consts.ACCESS_TOKEN_LIFESPAN_MINS)
        };
        
        var accessToken = _tokenService.Generate(tokenParams);
        tokenParams.Claims = new ClaimsIdentity(
                new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                }
            );
        tokenParams.ExpiryDateTime = DateTime.Now.AddDays(Consts.ACCESS_TOKEN_LIFESPAN_DAYS);

        var refreshToken = _tokenService.Generate(tokenParams);

        return new JwtTokenDto {
            Access = accessToken,
            Refresh = refreshToken
        };
    }
}
