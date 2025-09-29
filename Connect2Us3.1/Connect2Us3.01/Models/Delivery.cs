using System;
using System.ComponentModel.DataAnnotations;

namespace Connect2Us3._01.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public string DriverId { get; set; }
        public virtual ApplicationUser Driver { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime? ActualDeliveryDate { get; set; }
    }
}