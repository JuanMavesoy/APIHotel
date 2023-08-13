using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public ReservationService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;   
        }

        public Reservation MakeReservation(ReservationFormDTO reservationFormDTO)
        {

            string guestName = "";
            var room = _unitOfWork.Repository<Room>().GetByID(reservationFormDTO.RoomId);
            if (room == null)
            {
                throw new Exception("Habitación no encontrada.");
            }
            if (!room.Status) 
            {
                throw new Exception("No se pueden hacer reservas en habitaciones inhabilitadas.");
            }

            var reservation = new Reservation
            {
                IdReservation = Guid.NewGuid(),
                CheckInDate = reservationFormDTO.CheckInDate,
                CheckOutDate = reservationFormDTO.CheckOutDate,
                NumberOfPeople = reservationFormDTO.NumberOfPeople,
                PaymentStatus = false,
                ReservationStatus = "Pendiente",
                RoomId = room.RoomId
            };

            _unitOfWork.Repository<Reservation>().Insert(reservation);
            _unitOfWork.Commit();

            foreach (var passengerDTO in reservationFormDTO.Guests)
            {
                if (passengerDTO.GuestType != 1 && passengerDTO.GuestType != 2)
                {
                    throw new Exception("El valor de GuestType debe ser 1 para Titular o 2 para Acompañante.");
                }
                var passenger = new Guest
                {
                    IdReservation = reservation.IdReservation,
                    GuestType = passengerDTO.GuestType,
                    FirtsName = passengerDTO.FirtsName,
                    LastName = passengerDTO.LastName,
                    Birthdate = passengerDTO.Birthdate,
                    Gender = passengerDTO.Gender,
                    DocumentType = passengerDTO.DocumentType,
                    NumberDocument = passengerDTO.NumberDocument,
                    Email = passengerDTO.Email,
                    PhoneNumber = passengerDTO.PhoneNumber
                };
                _unitOfWork.Repository<Guest>().Insert(passenger);
                if (passenger.GuestType == 1)
                {
                    guestName = $"{passengerDTO.FirtsName} {passengerDTO.LastName}";
                }
               
            }

            room.Status = false;
            _unitOfWork.Repository<Room>().Update(room);

            var emailMessage = GenerateEmailMessage(reservation, guestName);
            _emailService.SendEmailAsync(reservationFormDTO.Email, "Confirmación de reserva", emailMessage).Wait();


            _unitOfWork.Commit();

            return reservation;
        }
        private string GenerateEmailMessage(Reservation reservation, string Name)
        {
           
            return $@"<body>
                    <h1>Confirmación de Reserva</h1>
                    <p>Estimado {Name}</p>
                    <p>Gracias por realizar su reserva en nuestro hotel. Aquí están los detalles de su reserva:</p>
                    <ul>
                        <li>Fecha de Check-In: {reservation.CheckInDate}</li>
                        <li>Fecha de Check-Out: {reservation.CheckOutDate}</li>
                    </ul>
                    <p>¡Esperamos que tenga una estancia agradable con nosotros!</p>
                    <p>Saludos,</p>
                    <p>El equipo de Prueba API Hotel</p>
                </body>";
        }

        public IEnumerable<ReservationDTO> GetReservationsByHotel(Guid hotelId)
        {
            var hotel = _unitOfWork.Repository<Hotel>().GetByID(hotelId);

            if (hotel == null)
            {
                throw new Exception("Hotel no encontrado.");
            }

            var reservations = _unitOfWork.Repository<Reservation>()
                        .Get(filter: r => r.Room.HotelId == hotelId, includeProperties: "Room,Guests")
                        .ToList();

            var reservationDtos = reservations.Select(reservation => new ReservationDTO
            {
                IdReservation = reservation.IdReservation,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                NumberOfPeople = reservation.NumberOfPeople,
                PaymentStatus = reservation.PaymentStatus,
                ReservationStatus = reservation.ReservationStatus,
                ReservationHolder = reservation.Guests.FirstOrDefault(guest => guest.GuestType == 1)?.FirtsName + " " + reservation.Guests.FirstOrDefault(guest => guest.GuestType == 1)?.LastName,
                EmergencyContactFullName = reservation.EmergencyContactFullName,
                EmergencyContactPhoneNumber = reservation.EmergencyContactPhoneNumber,
                Rooms = new List<RoomDTO>
                        {
                            new RoomDTO
                            {
                                RoomId = reservation.Room.RoomId,
                                NumberRoom = reservation.Room.NumberRoom,
                                Type = reservation.Room.Type,
                                PeopleAllowed = reservation.Room.PeopleAllowed,
                                BaseCost = reservation.Room.BaseCost,
                                Tax = reservation.Room.Tax,
                                Status = reservation.Room.Status,
                                Location = reservation.Room.Location,
                                HotelId  = reservation.Room.HotelId
                            }
                        }
            }).ToList();

            return reservationDtos;

        }

    }
}
