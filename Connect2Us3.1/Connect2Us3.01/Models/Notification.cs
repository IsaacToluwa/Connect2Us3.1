using System;
using System.ComponentModel.DataAnnotations;

namespace Connect2Us3._01.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime Date { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}