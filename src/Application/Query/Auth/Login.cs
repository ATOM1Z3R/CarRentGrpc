using Application.Dtos.Read;
using MediatR;

namespace Application.Query.Auth;

public record Login(string Email, string Password, string Key) : IRequest<JwtTokenDto>;
