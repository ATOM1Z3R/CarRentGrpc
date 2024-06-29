using MediatR;

namespace Application.Query.Auth;

public record RegenerateAccessToken(string refreshToken, string Key) : IRequest<string>;
