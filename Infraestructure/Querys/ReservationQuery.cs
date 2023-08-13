using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Querys
{
    public class ReservationQuery : IReservationQuery
    {
        internal HotelDbContext context;

        public ReservationQuery(HotelDbContext context)
        {
            this.context = context;
        }


        public IEnumerable<ReservationDetailDTO> GetDetaileReservation(Guid reservationId)
        {
            var reservationDetails = (from reservation in context.Reservations
                                      where reservation.IdReservation == reservationId
                                      select new ReservationDetailDTO
                                      {
                                          IdReservation = reservation.IdReservation,
                                          CheckInDate = reservation.CheckInDate,
                                          CheckOutDate = reservation.CheckOutDate,
                                          NumberOfPeople = reservation.NumberOfPeople,
                                          PaymentStatus = reservation.PaymentStatus,
                                          ReservationHolder = reservation.Guests.Where(guest => guest.GuestType == 1).Select(guest => guest.FirtsName + " " + guest.LastName).FirstOrDefault()?? string.Empty,
                                          EmergencyContactFullName = reservation.EmergencyContactFullName,
                                          EmergencyContactPhoneNumber = reservation.EmergencyContactPhoneNumber,
                                          ReservationStatus = reservation.ReservationStatus,
                                          
                                          Room = new RoomDetailedDTO
                                          {
                                              RoomId = reservation.Room.RoomId,
                                              NumberRoom = reservation.Room.NumberRoom,
                                              Type = reservation.Room.Type,
                                              PeopleAllowed = reservation.Room.PeopleAllowed,
                                              BaseCost = reservation.Room.BaseCost,
                                              Tax = reservation.Room.Tax,
                                              Status = reservation.Room.Status,
                                              Location = reservation.Room.Location,
                                              HotelId = reservation.Room.HotelId,
                                              Guest = reservation.Guests.Select(guest => new GuestDTO
                                              {
                                                  GuestType = guest.GuestType,
                                                  FirtsName = guest.FirtsName,
                                                  LastName = guest.LastName,
                                                  Birthdate = guest.Birthdate,
                                                  Gender = guest.Gender,
                                                  DocumentType = guest.DocumentType,
                                                  NumberDocument = guest.NumberDocument,
                                                  Email = guest.Email,
                                                  PhoneNumber = guest.PhoneNumber
                                              }).ToList()
                                          }
                                      }).ToList();

            return reservationDetails;
        }

        private string GetReservationHolderName(Reservation reservation)
        {
            var reservationHolder = reservation.Guests.FirstOrDefault(guest => guest.GuestType == 1);

            if (reservationHolder != null)
            {
                return reservationHolder.FirtsName + " " + reservationHolder.LastName;
            }

            return string.Empty;
        }

    }
}
