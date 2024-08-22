using FluentValidation;

namespace BookingRoom.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
    {
        public CreateBookingRequestValidator()
        {
            RuleFor(request => request.timeSlotsId)
                .NotEmpty()
                .NotNull()
                .Must(c => c.Count() > 0)
                .WithMessage("timeSlotsId is required.");

            RuleFor(request => request.roomId)
              .NotEmpty()
              .NotNull()
              .WithMessage("roomId is required.");

            RuleFor(request => request.userId)
              .NotEmpty()
              .NotNull()
              .WithMessage("userId is required.");
        }
    }

}
