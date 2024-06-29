using System.Security.Claims;

namespace Application.Dtos.Read;

public class GenerateTokenParamsDto
{
    public ClaimsIdentity Claims { get; set; } = new ClaimsIdentity();

    public string Key { get; set; } = string.Empty;

    public DateTime ExpiryDateTime { get; set; }
}
