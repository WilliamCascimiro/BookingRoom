namespace BookingRoom.Application.DTOs.Auth
{
    public class UserLoginDTOOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
