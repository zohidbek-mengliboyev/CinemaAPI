using System;

namespace CinemaAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Phone { get; set; }
        public DateTime ReservationTime { get; set }

        public int MovieId { get; set; }
        public int UserId { get; set; }
    }
}
