using System.Collections.Generic;

namespace Connect2Us3._01.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public string UserId { get; set; }
        public int CartId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public List<CartItem> CartItems { get; set; }
    }

    public class CartItem
    {
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateAdded { get; set; }
    }
}