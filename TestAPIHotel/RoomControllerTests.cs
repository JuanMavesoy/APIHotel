using APIHotel.Controllers;
using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace TestAPIHotel
{
    public class RoomControllerTests
    {
        [Fact]
        public void GetRoomsByHotel_ValidHotelId_ReturnsOk()
        {
         
            var roomServiceMock = new Mock<IRoomService>();
            var controller = new RoomController(roomServiceMock.Object);
            var hotelId = Guid.NewGuid();

            roomServiceMock.Setup(service => service.GetRoomsByHotel(hotelId))
                .Returns(new List<RoomDTO>());

            var result = controller.GetRoomsByHotel(hotelId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AssignRoomsToHotel_ValidInput_ReturnsOk()
        {
           
            var roomServiceMock = new Mock<IRoomService>();
            var controller = new RoomController(roomServiceMock.Object);
            var assignRoomsDTO = new AssignRoomsDTO
            {
                HotelId = Guid.NewGuid(),
                Rooms = new List<CreateRoomDTO>()
            };

            roomServiceMock.Setup(service => service.AssignRoomsToHotel(assignRoomsDTO.Rooms, assignRoomsDTO.HotelId))
                .Verifiable();

            var result = await controller.AssignRoomsToHotel(assignRoomsDTO);

            Assert.IsType<OkObjectResult>(result);
            roomServiceMock.Verify();
        }

        [Fact]
        public async Task UpdateRoom_ValidInput_ReturnsOk()
        {
            var roomServiceMock = new Mock<IRoomService>();
            var controller = new RoomController(roomServiceMock.Object);
            var updateRoomDTO = new UpdateRoomDTO();

            roomServiceMock.Setup(service => service.UpdateRoom(updateRoomDTO))
                .Verifiable();

            var result = await controller.UpdateRoom(updateRoomDTO);

            Assert.IsType<OkObjectResult>(result);
            roomServiceMock.Verify();
        }

        [Fact]
        public async Task ToggleRoomStatus_ValidInput_ReturnsOk()
        {
            var roomServiceMock = new Mock<IRoomService>();
            var controller = new RoomController(roomServiceMock.Object);
            var roomId = Guid.NewGuid();
            var status = true;

            roomServiceMock.Setup(service => service.ToggleRoomStatus(roomId, status))
                .Verifiable();

            var result = await controller.ToggleRoomStatus(roomId, status);

            Assert.IsType<OkObjectResult>(result);
            roomServiceMock.Verify();
        }
    }
}
