namespace BookingRoom.Application.DTOs.Auth
{
    public class AuthenticatedResponse
    {
        public AuthenticatedResponse(string token)
        {
            this.token = token;
        }

        public string token { get; set; }
    }

}
