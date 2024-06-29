using Application.Query.Auth;
using Grpc;
using Grpc.Core;
using MediatR;

namespace GrpcInterface.Services;

public class AuthService : Auth.AuthBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public AuthService(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        var login = new Login(
            request.Email,
            request.Password,
            _configuration["Jwt:Key"] ?? ""
        );
        var response = await _mediator.Send(login);
        return new LoginResponse
        {
            AccessToken = response.Access,
            RefreshToken = response.Refresh,
        };
    }

    public override async Task<RegenerateAccessTokenResponse> RegenerateAccessToken(RegenerateAccessTokenRequest request, ServerCallContext context)
    {
        var user = context.GetHttpContext().User;
        var regToken = new RegenerateAccessToken(
            request.RefreshToken,
            _configuration["Jwt:Key"] ?? ""
        );
        await _mediator.Send(regToken);
        return new RegenerateAccessTokenResponse
        {
            AccessToken = await _mediator.Send(regToken),
        };

    }
}
