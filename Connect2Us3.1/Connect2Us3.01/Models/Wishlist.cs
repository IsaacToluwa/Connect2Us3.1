using System.ComponentModel.DataAnnotations;

namespace Connect2Us3._01.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}