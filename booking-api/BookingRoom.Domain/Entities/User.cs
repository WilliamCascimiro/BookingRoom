using System.Drawing;
using System.Xml.Linq;

namespace BookingRoom.Domain.Entities
{
    public class User : BaseDomain
    {
        public User()
        {
        }

        public User(string name, string email, string senha, string role)
        {
            Id = Guid.NewGuid();
            this.Name = name;
            this.Email = email;
            this.Password = senha;
            this.Role = role;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<Booking> Reservas { get; set; }
    }

}
