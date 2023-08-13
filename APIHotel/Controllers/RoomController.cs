using Application.Services;
using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        public readonly IRoomService RoomService;

        public RoomController(IRoomService roomService)
        {
            this.RoomService = roomService;
        }

        [HttpGet("ListarHabitacionesPorHotel")]
        public IActionResult GetRoomsByHotel(Guid hotelId)
        {
            try
            {
                var rooms = RoomService.GetRoomsByHotel(hotelId);
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpPost("CrearHabitaciones")]
        public async Task<IActionResult> AssignRoomsToHotel([FromBody] AssignRoomsDTO assignRoomsDTO)
        {
            try
            {
                await RoomService.AssignRoomsToHotel(assignRoomsDTO.Rooms, assignRoomsDTO.HotelId);
                return Ok(new { mensaje = "Habitaciones asignadas correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("ModificarHabitacion")]
        public async Task<IActionResult> UpdateRoom([FromBody] UpdateRoomDTO updateRoomDTO)
        {
            try
            {
                await RoomService.UpdateRoom(updateRoomDTO);
                return Ok(new { mensaje = "Habitación modificada correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpPut("HabilitarDeshabilitarHabitacion")]
        public async Task<IActionResult> ToggleRoomStatus( Guid roomId,  bool status)
        {
            try
            {
                await RoomService.ToggleRoomStatus(roomId, status);
                return Ok(new { mensaje = "Estado de la habitación actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


    }
}
