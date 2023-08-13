using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Guest
    {
        public Guid GuestId { get; set; }
        public int GuestType { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public Char  Gender { get; set; }
        public string DocumentType { get; set; }
        public string NumberDocument { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Guid IdReservation { get; set; }
        public Reservation Reservation { get; set; }

    }
}
    