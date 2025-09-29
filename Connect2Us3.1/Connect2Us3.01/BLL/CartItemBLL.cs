using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using System.Collections.Generic;
using System.Linq;

namespace Connect2Us3._01.BLL
{
    public class CartItemBLL
    {
        private ApplicationDbContext _context;

        public CartItemBLL()
        {
            _context = new ApplicationDbContext();
        }

        public List<CartItem> GetAllCartItems()
        {
            return _context.CartItems.ToList();
        }

        public CartItem GetCartItemById(int id)
        {
            return _context.CartItems.Find(id);
        }

        public void CreateCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _context.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCartItem(int id)
        {
            var cartItem = _context.CartItems.Find(id);
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }
    }
}