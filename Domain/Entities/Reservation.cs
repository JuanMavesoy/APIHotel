using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reservation
    {
        public Guid IdReservation { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfPeople { get; set; }
        public bool PaymentStatus { get; set; }
        public string ReservationStatus { get; set; }
        public string? EmergencyContactFullName { get; set; }
        public string? EmergencyContactPhoneNumber { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }

        public ICollection<Guest> Guests { get; set; }
    }
}
