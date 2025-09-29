using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using System.Collections.Generic;
using System.Linq;

namespace Connect2Us3._01.BLL
{
    public class WishlistBLL
    {
        private ApplicationDbContext _context;

        public WishlistBLL()
        {
            _context = new ApplicationDbContext();
        }

        public List<Wishlist> GetAllWishlists()
        {
            return _context.Wishlists.ToList();
        }

        public Wishlist GetWishlistById(int id)
        {
            return _context.Wishlists.Find(id);
        }

        public void CreateWishlist(Wishlist wishlist)
        {
            _context.Wishlists.Add(wishlist);
            _context.SaveChanges();
        }

        public void UpdateWishlist(Wishlist wishlist)
        {
            _context.Entry(wishlist).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteWishlist(int id)
        {
            var wishlist = _context.Wishlists.Find(id);
            _context.Wishlists.Remove(wishlist);
            _context.SaveChanges();
        }
    }
}