using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{

    public class GuestDTO
    {
        public int GuestType { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public Char Gender { get; set; }
        public string DocumentType { get; set; }
        public string NumberDocument { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

}
