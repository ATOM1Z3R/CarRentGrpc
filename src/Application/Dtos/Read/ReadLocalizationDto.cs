using Application.Dtos.Common;
using Domain.Models;

namespace Application.Dtos.Read;

public class ReadLocalizationDto : LocalizationDto
{
    public int Id { get; }

    public ReadLocalizationDto(Localization localization)
    {
        Id = localization.Id;
        City = localization.City;
        Street = localization.Street;
    }

    public static implicit operator ReadLocalizationDto? (Localization localization)
    => localization != null ? new ReadLocalizationDto(localization) : null;
}
