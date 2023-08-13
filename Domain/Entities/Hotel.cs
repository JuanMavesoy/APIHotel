using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hotel
    {
        public Guid HotelId { get; set; }
        public string NameHotel { get; set; }
        public bool Enabled { get; set; }
        public string City { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
