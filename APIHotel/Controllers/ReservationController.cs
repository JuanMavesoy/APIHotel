using Application.Services;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        public readonly IReservationService reservationService;
        public readonly IReservationQuery reservationQuery;

        public ReservationController(IReservationService reservationService, IReservationQuery reservationQuery)
        {
            this.reservationService = reservationService;
            this.reservationQuery = reservationQuery;
        }


        [HttpPost("RealizarReserva")]
        public IActionResult MakeReservation([FromBody] ReservationFormDTO reservationFormDTO)
        { 
             try
             {
                 var reservation = reservationService.MakeReservation(reservationFormDTO);
                 return Ok(new { mensaje = "Reserva realizada con éxito. Se ha enviado una confirmación por correo electrónico." });
             }
            catch (Exception ex)
             {
                 return BadRequest(new { mensaje = ex.Message });
             }
        }

        [HttpGet("ListarReservasPorHotel")]
        public IActionResult GetReservationsByHotel(Guid hotelId)
        {
            try
            {
                var reservations = reservationService.GetReservationsByHotel(hotelId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("DetalleReserva")]
        public IActionResult GetDetailedReservation(Guid reservationId)
        {
            try
            {
                var reservations = reservationQuery.GetDetaileReservation(reservationId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
