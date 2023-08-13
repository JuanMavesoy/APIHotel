using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ReservationDTO
    {
        public Guid IdReservation { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfPeople { get; set; }
        public bool PaymentStatus { get; set; }
        public string ReservationStatus { get; set; }
        
        public string ReservationHolder { get; set; }
        public string? EmergencyContactFullName { get; set; }
        public string? EmergencyContactPhoneNumber { get; set; }
        public List<RoomDTO> Rooms { get; set; }
    }

    public class SearchCriteria
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfPeople { get; set; }
        public string City { get; set; }
    }

    public class ReservationFormDTO
    {
        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfPeople { get; set; }
        public string Email { get; set; }
        public string EmergencyContactFullName { get; set; }
        public string EmergencyContactPhoneNumber { get; set; }
        public List<GuestDTO> Guests { get; set; }
    }

    public class ReservationDetailDTO
    {
        public Guid IdReservation { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfPeople { get; set; }
        public bool PaymentStatus { get; set; }
        public string ReservationStatus { get; set; }
        public string ReservationHolder { get; set; }
        public string? EmergencyContactFullName { get; set; }
        public string? EmergencyContactPhoneNumber { get; set; }
        public RoomDetailedDTO Room { get; set; }
    }
}
