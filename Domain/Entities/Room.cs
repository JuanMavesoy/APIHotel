using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public int NumberRoom { get; set; }
        public int Type { get; set; } 
        public int PeopleAllowed { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Tax { get; set; }
        public bool Status { get; set; }
        public string? Location { get; set; }
        public Guid HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }


}





