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
    public class RoomService : IRoomService
    {

        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<RoomDTO> GetRoomsByHotel(Guid hotelId)
        {
            var hotel = _unitOfWork.Repository<Hotel>().GetByID(hotelId);

            if (hotel == null)
            {
                throw new Exception("Hotel no encontrado.");
            }

            var rooms = _unitOfWork.Repository<Room>().Get(filter: r => r.HotelId == hotelId);

            var roomDtos = rooms.Select(room => new RoomDTO
            {
                RoomId = room.RoomId,
                NumberRoom = room.NumberRoom,
                Type = room.Type,
                PeopleAllowed = room.PeopleAllowed,
                BaseCost = room.BaseCost,
                Tax = room.Tax,
                Status = room.Status,
                Location = room.Location,
                HotelId     = hotelId

            }).ToList();

            return roomDtos;
        }


        public async Task AssignRoomsToHotel(List<CreateRoomDTO> createRoomDtos, Guid hotelId)
        {
            var hotel = _unitOfWork.Repository<Hotel>().GetByID(hotelId);
            if (hotel == null)
            {
                throw new Exception("Hotel no encontrado.");
            }
            if (!hotel.Enabled)
            {
                throw new Exception("No se pueden asignar habitaciones a un hotel deshabilitado.");
            }

            foreach (var createRoomDto in createRoomDtos)
            {
                var existingRoom = _unitOfWork.Repository<Room>().Get(u => u.HotelId == hotelId && u.NumberRoom == createRoomDto.NumberRoom);

                if (existingRoom.Count() != 0)
                {
                    throw new Exception($"La habitación {createRoomDto.NumberRoom} ya existe.");
                }

                var newRoom = new Room
                {
                    RoomId = Guid.NewGuid(),
                    NumberRoom = createRoomDto.NumberRoom,
                    Type = createRoomDto.Type,
                    PeopleAllowed = createRoomDto.PeopleAllowed,
                    BaseCost = createRoomDto.BaseCost,
                    Tax = createRoomDto.Tax,
                    Status = createRoomDto.Status,
                    Location = createRoomDto.Location,
                    HotelId = hotelId
                };

                _unitOfWork.Repository<Room>().Insert(newRoom);
            }

            await _unitOfWork.Commit();
        }

        public async Task UpdateRoom(UpdateRoomDTO updateRoomDTO)
        {
            var room = _unitOfWork.Repository<Room>().GetByID(updateRoomDTO.RoomId);

            if (room == null)
            {
                throw new Exception("Habitación no encontrada.");
            }

            room.NumberRoom = updateRoomDTO.NumberRoom;
            room.Type = updateRoomDTO.Type;
            room.PeopleAllowed = updateRoomDTO.PeopleAllowed;
            room.BaseCost = updateRoomDTO.BaseCost;
            room.Tax = updateRoomDTO.Tax;
            room.Status = updateRoomDTO.Status;
            room.Location = updateRoomDTO.Location;

            _unitOfWork.Repository<Room>().Update(room);
            await _unitOfWork.Commit();
        }


        public async Task ToggleRoomStatus(Guid roomId, bool status)
        {
            var room = _unitOfWork.Repository<Room>().GetByID(roomId);

            if (room == null)
            {
                throw new Exception("Habitación no encontrada.");
            }
            room.Status = status;

            _unitOfWork.Repository<Room>().Update(room);
            await _unitOfWork.Commit();
        }
    }
}
