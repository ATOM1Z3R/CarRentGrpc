using Application.Dtos.Common;
using Domain.Models;

namespace Application.Dtos.Write;

public sealed class WriteLocalizationDto : LocalizationDto
{
    public Localization AsLocalization()
    {
        return new Localization() {
            City = City,
            Street = Street
        };
    }

    public static implicit operator Localization(WriteLocalizationDto localizationDto)
    => localizationDto.AsLocalization();
}
