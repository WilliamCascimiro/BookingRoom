using BookingRoom.Application.Features.Bookings.Queries.Requests;
using BookingRoom.Application.Features.Bookings.Queries.Responses;

namespace BookingRoom.Application.Features.Bookings.Handlers
{
    public interface IFindCustomerByIdHandler
    {
        FindCustomerByIdResponse Handle(FindCustomerByIdRequest command);
    }

}
