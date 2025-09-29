using System;
using System.ComponentModel.DataAnnotations;

namespace Connect2Us3._01.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Status { get; set; }
    }
}