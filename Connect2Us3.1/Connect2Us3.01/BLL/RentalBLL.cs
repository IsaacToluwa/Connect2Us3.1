using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using System.Collections.Generic;
using System.Linq;

namespace Connect2Us3._01.BLL
{
    public class RentalBLL
    {
        private ApplicationDbContext _context;

        public RentalBLL()
        {
            _context = new ApplicationDbContext();
        }

        public List<Rental> GetAllRentals()
        {
            return _context.Rentals.ToList();
        }

        public Rental GetRentalById(int id)
        {
            return _context.Rentals.Find(id);
        }

        public void CreateRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }

        public void UpdateRental(Rental rental)
        {
            _context.Entry(rental).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteRental(int id)
        {
            var rental = _context.Rentals.Find(id);
            _context.Rentals.Remove(rental);
            _context.SaveChanges();
        }
    }
}