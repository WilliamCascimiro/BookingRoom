using BookingRoom.Application.DTOs.Booking;
using BookingRoom.Application.Features.Common;
using BookingRoom.Domain.Entities;
using BookingRoom.Domain.Interfaces;

namespace BookingRoom.Application.Services
{
    public interface IBookingService
    {
        Task<Result<BookingDTOOutput>> Create(CreateBookingRequestOld reserva);
        Task<Result<BookingDTOOutput>> Update(UpdateBookingRequest updateBookingRequest);
        Task<Result<IEnumerable<ListBookingResponse>>> ListAllBookingsByUser(string UserId);
        Task<Result<DetailBookingResponse>> GetBookingById(string bookingId);
        Task<IEnumerable<Booking>> ListAllBookings();
        Task<Result<IEnumerable<ListBookingResponse>>> ListBookingsFromAllUsers();
        Task<Result<bool>> Delete(string bookingId);
    }
}
