using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HotelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Hotel> CreateHotel(CreateHotelDTO CreateHotelDTO)
        {
            var newHotel = new Hotel()
            {
                HotelId = Guid.NewGuid(),
                NameHotel = CreateHotelDTO.NameHotel,
                City = CreateHotelDTO.City,
                Enabled = true

            };
            _unitOfWork.Repository<Hotel>().Insert(newHotel);
            await _unitOfWork.Commit();

            return newHotel;
        }

        public async Task<IEnumerable<HotelDTO>> GetHotels()
        {
            var hotels = _unitOfWork.Repository<Hotel>().Get(includeProperties: "Rooms").ToList();

            var hotelDtos = hotels.Select(hotel => new HotelDTO
            {
                HotelId = hotel.HotelId,
                NameHotel = hotel.NameHotel,
                City = hotel.City,
                Enabled = hotel.Enabled,
                Rooms = hotel.Rooms.Select(room => new RoomDTO
                {
                    RoomId = room.RoomId,
                    NumberRoom = room.NumberRoom,
                    Type = room.Type,
                    PeopleAllowed = room.PeopleAllowed,
                    BaseCost = room.BaseCost,
                    Tax = room.Tax,
                    Status = room.Status,
                    Location = room.Location,
                    HotelId  = room.HotelId
                    
                }).ToList()
            }).ToList();

            return hotelDtos;
        }
        public async Task UpdateHotel(UpdateHotelDTO updateHotelDTO)
        {
            var hotel = _unitOfWork.Repository<Hotel>().GetByID(updateHotelDTO.HotelId);

            if (hotel == null)
            {
                throw new Exception("Hotel no encontrado.");
            }

            hotel.NameHotel = updateHotelDTO.NameHotel;
            hotel.Enabled = updateHotelDTO.Enabled;
            hotel.City = updateHotelDTO.City;

            _unitOfWork.Repository<Hotel>().Update(hotel);
            await _unitOfWork.Commit();
        }

        public async Task ToggleHotelStatus(Guid hotelId, bool enabled)
        {
            var hotel = _unitOfWork.Repository<Hotel>().GetByID(hotelId);

            if (hotel == null)
            {
                throw new Exception("Hotel no encontrado.");
            }

            hotel.Enabled = enabled;

            _unitOfWork.Repository<Hotel>().Update(hotel);
            await _unitOfWork.Commit();
        }

    }
}
