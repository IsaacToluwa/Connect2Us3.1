using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using System.Collections.Generic;
using System.Linq;

namespace Connect2Us3._01.BLL
{
    public class ShoppingCartBLL
    {
        private ApplicationDbContext _context;

        public ShoppingCartBLL()
        {
            _context = new ApplicationDbContext();
        }

        public List<ShoppingCart> GetAllShoppingCarts()
        {
            return _context.ShoppingCarts.ToList();
        }

        public ShoppingCart GetShoppingCartById(int id)
        {
            return _context.ShoppingCarts.Find(id);
        }

        public void CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            _context.SaveChanges();
        }

        public void UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.Entry(shoppingCart).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteShoppingCart(int id)
        {
            var shoppingCart = _context.ShoppingCarts.Find(id);
            _context.ShoppingCarts.Remove(shoppingCart);
            _context.SaveChanges();
        }
    }
}