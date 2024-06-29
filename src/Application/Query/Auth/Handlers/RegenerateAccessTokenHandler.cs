using System.Security.Claims;
using Application.Dtos.Read;
using Application.Services;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Auth.Handlers;

public class RegenerateAccessTokenHandler : IRequestHandler<RegenerateAccessToken, string>
{
    private readonly IUnitOfWork _uow;

    private readonly ITokenService _tokenService;

    public RegenerateAccessTokenHandler(IUnitOfWork uow, ITokenService tokenService)
    {
        _uow = uow;
        _tokenService = tokenService;
    }
    
    public async Task<string> Handle(RegenerateAccessToken request, CancellationToken cancellationToken)
    {
        var claims = _tokenService.Validate(request.refreshToken, request.Key);

        var claimEmployeeId = claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var claimIsParsed = Int32.TryParse(claimEmployeeId, out int employeeId);
        
        if (!claimIsParsed)
        {
            throw new InvalidTokenException();
        }
        var employee = await _uow.Employees.GetByIdAsync(employeeId);
        if (employee is null)
        {
            throw new EmployeeWithGivenIdDoesntExistException(employeeId);
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

        return _tokenService.Generate(tokenParams);
    }
}
