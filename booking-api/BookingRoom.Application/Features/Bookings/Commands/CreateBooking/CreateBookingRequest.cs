using BookingRoom.Application.Features.Common;
using MediatR;

namespace BookingRoom.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingRequest : IRequest<Result<CreateBookingResponse>>
    {
        public List<string>? timeSlotsId { get; set; }
        public string? roomId { get; set; }
        public string? userId { get; set; }
    }

}
