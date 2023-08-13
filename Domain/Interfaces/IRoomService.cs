using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRoomService
    {
        Task AssignRoomsToHotel(List<CreateRoomDTO> createRoomDtos, Guid hotelId);
        Task UpdateRoom(UpdateRoomDTO updateRoomDTO);
        Task ToggleRoomStatus(Guid roomId, bool status);
        IEnumerable<RoomDTO> GetRoomsByHotel(Guid hotelId);
    }
}

