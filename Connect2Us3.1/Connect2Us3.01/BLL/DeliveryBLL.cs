using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using System.Collections.Generic;
using System.Linq;

namespace Connect2Us3._01.BLL
{
    public class DeliveryBLL
    {
        private ApplicationDbContext _context;

        public DeliveryBLL()
        {
            _context = new ApplicationDbContext();
        }

        public List<Delivery> GetAllDeliveries()
        {
            return _context.Deliveries.ToList();
        }

        public Delivery GetDeliveryById(int id)
        {
            return _context.Deliveries.Find(id);
        }

        public void CreateDelivery(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            _context.SaveChanges();
        }

        public void UpdateDelivery(Delivery delivery)
        {
            _context.Entry(delivery).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteDelivery(int id)
        {
            var delivery = _context.Deliveries.Find(id);
            _context.Deliveries.Remove(delivery);
            _context.SaveChanges();
        }
    }
}