using Domain;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Querys
{
    public class HotelQuery : IHotelQuery
    {
        internal HotelDbContext context;
        public HotelQuery(HotelDbContext context)
        {
            this.context = context;

        }

        public IEnumerable<HotelDTO> SearchHotels(SearchCriteria dto)
        {
            var hotels = (from hotel in context.Hotels
                          where hotel.City == dto.City && hotel.Enabled == true
                          select new HotelDTO
                          {
                              HotelId = hotel.HotelId,
                              City = hotel.City,
                              NameHotel = hotel.NameHotel,
                              Enabled  = hotel.Enabled,
                              Rooms = (List<RoomDTO>)hotel.Rooms.Where(r => r.PeopleAllowed >= dto.NumberOfPeople &&
                              r.Status == true &&
                             !r.Reservations.Any(reservation =>
                                    dto.CheckInDate < reservation.CheckOutDate && reservation.CheckInDate < dto.CheckOutDate))
                              .Select(r => new RoomDTO
                              {
                                  RoomId = r.RoomId,
                                  NumberRoom = r.NumberRoom,
                                  Type = r.Type,
                                  PeopleAllowed = r.PeopleAllowed,
                                  BaseCost = r.BaseCost,
                                  Tax = r.Tax,
                                  Status = r.Status,
                                  Location = r.Location,
                                  HotelId  = r.HotelId

                              })
                          }).ToList();


            return hotels.Where(hotel => hotel.Rooms.Any()).ToList();
        }
    }
}
