namespace Application.Dtos.Common;

public class CarDto
{
    public string NumberPlate { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public uint NumberOfSeats { get; set; }

    public uint Year { get; set; }

    public bool Availability { get; set; }

    public uint PriceMultiplier { get; set; }
}
