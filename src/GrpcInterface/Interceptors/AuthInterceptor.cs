using Application.Services;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.IdentityModel.Tokens;

namespace GrpcInterface.Interceptors;

public class AuthInterceptor : Interceptor
{
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthInterceptor(ITokenService tokenService, IConfiguration configuration)
    {
        _configuration = configuration;
        _tokenService = tokenService;
    }
    
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation
    )
    {
        var authHeader = context.RequestHeaders.FirstOrDefault(x => x.Key == "authorization");
        if (authHeader is null)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "Unauthenticated"));
        }
        var token = authHeader.Value.Split(' ').LastOrDefault() ?? "";
        try
        {
            var claims = _tokenService.Validate(token, _configuration["Jwt:Key"] ?? "");
            foreach (var claim in claims.Claims)
            {
                context.GetHttpContext().Items.Add(claim.Type, claim.Value);
            }
            return await continuation(request, context);
        }
        catch (SecurityTokenExpiredException)
        {
            throw new UnauthorizedAccessException("Token expired");
        }
    }
}
