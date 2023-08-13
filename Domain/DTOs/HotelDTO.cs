using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class HotelDTO
    {
        public Guid HotelId { get; set; }
        public string NameHotel { get; set; }
        public bool Enabled { get; set; }
        public string City { get; set; }
        public List<RoomDTO> Rooms { get; set; }
    }

    public class CreateHotelDTO
    {
        public string NameHotel { get; set; }
        public string City { get; set; }
    }

    public class UpdateHotelDTO
    {
        public Guid HotelId { get; set; }
        public string NameHotel { get; set; }
        public bool Enabled { get; set; }
        public string City { get; set; }
    }
}
