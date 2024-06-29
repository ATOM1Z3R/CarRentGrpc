using Application.Dtos.Read;
using Application.Dtos.Update;
using Application.Dtos.Write;
using Google.Protobuf.WellKnownTypes;
using Grpc;

namespace GrpcInterface.MessagesMappers;

public static class CustomerServiceMappers
{
    public static GetCustomerByIdResponse ReadCustomerDtoToGetCustomerByIdResponse(ReadCustomerDetailDto customer)
    {
        if (customer is null)
        {
            return new GetCustomerByIdResponse();
        }
        var response = new GetCustomerByIdResponse
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Customer = new CustomerCommon
            {
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                City = customer.City,
                Street = customer.Street,
            },
        };
        var customerReservation = customer.Reservations.Select(reservation => new CustomerReservation
        {
            Id = reservation.Id,
            RentDate = reservation.RentDate.ToTimestamp(),
            ActualRetrieveDate = reservation.ActualRetrieveDate.ToTimestamp(),
            ExpectingRetrieveDate = reservation.ExpectingRetrieveDate.ToTimestamp(),
            ReservedCar = new CustomerReservation.Types.ReservedCar
            {
                NumberPlate = reservation.Car.NumberPlate,
                Manufacturer = reservation.Car.Manufacturer,
                Model = reservation.Car.Model,
            },
        });
        response.Reservations.AddRange(customerReservation);
        return response;
    }

    public static UpdateCustomerDto UpdateCustomerRequestToUpdateCustomerDto(UpdateCustomerRequest request)
    {
        return new UpdateCustomerDto
        {
            Id = request.Id,
            PhoneNumber = request.Customer.PhoneNumber,
            Email = request.Customer.Email,
            City = request.Customer.City,
            Street = request.Customer.Street,
        };
    }

    public static WriteCustomerDto AddCustomerRequestToWriteCustomerDto(AddCustomerRequest request)
    {
        return new WriteCustomerDto
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Customer.Email,
            PhoneNumber = request.Customer.PhoneNumber,
            City = request.Customer.City,
            Street = request.Customer.Street,
        };
    }
}
