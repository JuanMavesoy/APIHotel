
using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Application.Services;
using Domain;

namespace APIHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        public readonly IHotelService HotelService;
        public readonly IHotelQuery hotelQuery;

        public HotelController(IHotelService hotelService, IHotelQuery hotelQuery)
        {
            this.HotelService = hotelService;
            this.hotelQuery = hotelQuery;
        }


        [HttpPost("CrearHotel")]
        public async Task<IActionResult> Post([FromBody] CreateHotelDTO CreateHotelDTO)
        {
            try
            {
                var newHotel = await HotelService.CreateHotel(CreateHotelDTO);

                return Ok(newHotel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("ListarHoteles")]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels = await HotelService.GetHotels();

                return Ok(hotels);

            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("ModificarHotel")]
        public async Task<IActionResult> UpdateHotel([FromBody] UpdateHotelDTO updateHotelDTO)
        {
            try
            {
                await HotelService.UpdateHotel(updateHotelDTO);
                return Ok(new { mensaje = "Hotel modificado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }
        [HttpPut("HabilitarDeshabilitarHotel")]
        public async Task<IActionResult> ToggleHotelStatus(Guid hotelId, bool enabled)
        {
            try
            {
                await HotelService.ToggleHotelStatus(hotelId, enabled);
                return Ok(new { mensaje = "Estado del hotel actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpGet("BuscarHotelesDisponibles")]
        public IActionResult SearchReservations([FromQuery] SearchCriteria criteria)
        {
            try
            {
                var reservations = hotelQuery.SearchHotels(criteria);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}
