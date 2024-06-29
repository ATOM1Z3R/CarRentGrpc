using Domain.Models;

namespace Application.Dtos.Read;

public class ReadCustomerDetailDto : ReadCustomerDto
{
    public ICollection<ReadReservationDto> Reservations { get; set; } = new List<ReadReservationDto>();

    public ReadCustomerDetailDto(Customer customer) : base(customer)
    {
        Reservations = customer.Reservations?
            .Select(x => new ReadReservationDto(x, DateTimeKind.Utc))
            .ToList() ?? new List<ReadReservationDto>();
    }

    public static implicit operator ReadCustomerDetailDto?(Customer customer)
    => customer is null ? null : new ReadCustomerDetailDto(customer);
}
