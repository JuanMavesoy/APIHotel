using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class RoomDTO
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
    }

    public class CreateRoomDTO
    {
        public int NumberRoom { get; set; }
        public int Type { get; set; }
        public int PeopleAllowed { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Tax { get; set; }
        public bool Status { get; set; }
        public string? Location { get; set; }
    }

    public class UpdateRoomDTO
    {
        public Guid RoomId { get; set; }
        public int NumberRoom { get; set; }
        public int Type { get; set; }
        public int PeopleAllowed { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Tax { get; set; }
        public bool Status { get; set; }
        public string Location { get; set; }
    }


    public class AssignRoomsDTO
    {
        public Guid HotelId { get; set; }
        public List<CreateRoomDTO> Rooms { get; set; }
    }


    public class RoomDetailedDTO
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
        public List<GuestDTO> Guest { get; set; }
    }
}
