using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connect2Us3._01.Models
{
    public class Rental
    {
        public int RentalId { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RentalFee { get; set; }
    }
}