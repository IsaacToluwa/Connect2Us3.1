using System;
using System.ComponentModel.DataAnnotations;

namespace Connect2Us3._01.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        public string ReviewText { get; set; }
    }
}