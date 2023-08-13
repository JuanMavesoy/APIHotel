using Domain.Entities;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Interfaces
{
    public interface IHotelService
    {
        Task<Hotel> CreateHotel(CreateHotelDTO CreateHotelDTO);
        Task<IEnumerable<HotelDTO>> GetHotels();
        Task UpdateHotel(UpdateHotelDTO updateHotelDTO);
        Task ToggleHotelStatus(Guid hotelId, bool enabled);
    }
}
