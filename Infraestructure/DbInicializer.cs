using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<HotelDbContext>();
   

            if (context.Hotels.Any())
            {
                return; // Ya se han insertado datos
            }
            var hotel1 = new Hotel
            {
                HotelId = new Guid("b48820b3-93df-41c8-8e04-05442e58aa75"),
                NameHotel = "Caqueta Real",
                City = "Florencia",
                Enabled = true
            };
            var hotel2 = new Hotel
            {
                HotelId = new Guid("8d6e8c76-4746-4b78-a660-292597e087fb"),
                NameHotel = "Neiva Plaza",
                City = "Neiva",
                Enabled = true
            };

            var room1 = new Room
            {
                RoomId = new Guid("496770f2-afc1-4cb0-af3a-cca0af6eeb70"),
                NumberRoom = 1,
                Type = 1,
                PeopleAllowed = 2,
                BaseCost = 3000,
                Tax = 10,
                Status = true,
                Location = "1 Piso",
                HotelId = hotel1.HotelId
            };

            var room2 = new Room
            {
                RoomId = new Guid("abb02fbe-48d8-4570-9377-b520b30192d3"),
                NumberRoom = 2,
                Type = 2,
                PeopleAllowed = 4,
                BaseCost = 6000,
                Tax = 10,
                Status = true,
                Location = "1 Piso",
                HotelId = hotel1.HotelId
            };

            var room3 = new Room
            {
                RoomId = new Guid("ff5cfd71-5a41-4a9d-875a-8f5deee2ed29"),
                NumberRoom = 3,
                Type = 3,
                PeopleAllowed = 6,
                BaseCost = 8000,
                Tax = 10,
                Status = true,
                Location = "2 Piso",
                HotelId = hotel1.HotelId
            };

            var reservation = new Reservation
            {
                IdReservation = new Guid("f47993c1-98cc-475e-9e48-5f7dfa55f590"),
                CheckInDate = new DateTime(2023, 08, 15),
                CheckOutDate = new DateTime(2023, 08, 20),
                NumberOfPeople = 2,
                PaymentStatus = false,
                ReservationStatus = "Pendiente",
                EmergencyContactFullName ="Armando obredor",
                EmergencyContactPhoneNumber = "32014569712",
                RoomId = room1.RoomId
            };

            var guest1 = new Guest
            {
                IdReservation = reservation.IdReservation,
                GuestId = new Guid("59742e1a-955c-4c94-b5ac-c3a26f4d1d78"),
                GuestType = 1,
                FirtsName = "Juan Carlos",
                LastName = "Mavesoy Orozco",
                Birthdate = new DateTime(2000, 08, 20),
                Gender = 'M',
                DocumentType = "CC",
                NumberDocument = "1117777777",
                Email = "jua.mavesoy@udla.edu.co",
                PhoneNumber = "3206515645"
            };
            var guest2 = new Guest
            {
                IdReservation = reservation.IdReservation,
                GuestId = new Guid("8ed0d95d-4ca2-44d6-a18b-c7cae9df05ce"),
                GuestType = 2,
                FirtsName = "Laura Valentina",
                LastName = "Sanchez Perdomo",
                Birthdate = new DateTime(2000, 08, 20),
                Gender = 'F',
                DocumentType = "CC",
                NumberDocument = "111666666",
                Email = "Lau.sanchez@udla.edu.co",
                PhoneNumber = "32201256854"
            };

            context.Hotels.AddRange(hotel1, hotel2);
            context.Rooms.AddRange(room1, room2, room3);
            context.Reservations.Add(reservation);
            context.Guests.AddRange(guest1, guest2);
            context.SaveChanges();
        }
    }
}
