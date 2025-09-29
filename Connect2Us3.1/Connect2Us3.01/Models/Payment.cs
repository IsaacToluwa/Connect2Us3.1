using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connect2Us3._01.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
    }
}