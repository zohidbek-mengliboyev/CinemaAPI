using System.Collections.Generic;

namespace CinemaAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
