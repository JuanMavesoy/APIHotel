using Domain.Interfaces;
using APIHotel.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Domain;

namespace TestAPIHotel
{
    public class HotelControllerTests
    {
        [Fact]
        public async Task Post_ValidHotel_ReturnsOkResult()
        {
            var mockHotelService = new Mock<IHotelService>();
            var controller = new HotelController(mockHotelService.Object, null);

            var createHotelDTO = new CreateHotelDTO
            {
                NameHotel = "Test Hotel",
                City = "Test City"
            };

            var result = await controller.Post(createHotelDTO);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetHotels_ReturnsOkResult()
        {
        
            var mockHotelService = new Mock<IHotelService>();
            var mockHotelQuery = new Mock<IHotelQuery>();

            mockHotelService.Setup(service => service.GetHotels())
                .ReturnsAsync(new List<HotelDTO>()); 

            var controller = new HotelController(mockHotelService.Object, mockHotelQuery.Object);

        
            var result = await controller.GetHotels() as OkObjectResult;

            
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result.Value);
            var hotels = result.Value as List<HotelDTO>;
            Assert.NotNull(hotels);
       
        }


    }
}
