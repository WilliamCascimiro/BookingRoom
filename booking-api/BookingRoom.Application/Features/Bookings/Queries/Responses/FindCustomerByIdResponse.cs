namespace BookingRoom.Application.Features.Bookings.Queries.Responses
{
    public class FindCustomerByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
