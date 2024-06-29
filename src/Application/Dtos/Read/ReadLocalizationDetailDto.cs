using Application.Dtos.Common;
using Domain.Models;

namespace Application.Dtos.Read;

public class ReadLocalizationDetailDto : LocalizationDto
{
    public int Id { get; set; }

    public List<ReadCarDto> Cars { get; set; } = new List<ReadCarDto>();

    public ReadLocalizationDetailDto(Localization localization)
    {
        Id = localization.Id;
        City = localization.City;
        Street = localization.Street;
        Cars = localization.Cars?.Select(x => new ReadCarDto(x))
                                   .ToList() ?? new List<ReadCarDto>();
    }
}
