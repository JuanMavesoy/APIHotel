using APIHotel.Controllers;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestAPIHotel
{
    public class ReservationControllerTests
    {
        [Fact]
        public void MakeReservation_ValidInput_ReturnsOk()
        {
            var reservationServiceMock = new Mock<IReservationService>();
            var reservationQueryMock = new Mock<IReservationQuery>();
            var controller = new ReservationController(reservationServiceMock.Object, reservationQueryMock.Object);
            var reservationFormDTO = new ReservationFormDTO
            {
                RoomId = Guid.Parse("ff5cfd71-5a41-4a9d-875a-8f5deee2ed29"),
                CheckInDate = DateTime.Parse("2023-08-08T21:07:23.166Z"),
                CheckOutDate = DateTime.Parse("2023-08-12T21:07:23.166Z"),
                NumberOfPeople = 1,
                Email = "jua.mavesoy@udla.edu.co",
                Guests = new List<GuestDTO>
                {
                                new GuestDTO
                                {
                                    GuestType = 1,
                                    FirtsName = "Juan",
                                    LastName = "Mavesoy",
                                    Birthdate = DateTime.Parse("2023-08-12T21:07:23.167Z"),
                                    Gender = 'M',
                                    DocumentType = "cc",
                                    NumberDocument = "111754228822",
                                    Email = "jua.mavesoy@udla.edu.co",
                                    PhoneNumber = "32015655232"
                                }
                }
             };

            reservationServiceMock.Setup(service => service.MakeReservation(reservationFormDTO))
                .Returns(new Reservation());

           
            var result = controller.MakeReservation(reservationFormDTO);

          
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetReservationsByHotel_ValidHotelId_ReturnsOk()
        {
          
            var reservationServiceMock = new Mock<IReservationService>();
            var reservationQueryMock = new Mock<IReservationQuery>();
            var controller = new ReservationController(reservationServiceMock.Object, reservationQueryMock.Object);
            var hotelId = Guid.NewGuid();

            reservationServiceMock.Setup(service => service.GetReservationsByHotel(hotelId))
                .Returns(new List<ReservationDTO>());

           
            var result = controller.GetReservationsByHotel(hotelId);

            
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetDetailedReservation_ValidReservationId_ReturnsOk()
        {
            var reservationServiceMock = new Mock<IReservationService>();
            var reservationQueryMock = new Mock<IReservationQuery>();
            var controller = new ReservationController(reservationServiceMock.Object, reservationQueryMock.Object);
            var reservationId = Guid.NewGuid();

            reservationServiceMock.Setup(service => service.GetReservationsByHotel(It.IsAny<Guid>()))
                    .Returns(new List<ReservationDTO> { new ReservationDTO(), new ReservationDTO() });

            var result = controller.GetDetailedReservation(reservationId);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
