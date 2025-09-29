using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using System.Collections.Generic;
using System.Linq;

namespace Connect2Us3._01.BLL
{
    public class PaymentBLL
    {
        private ApplicationDbContext _context;

        public PaymentBLL()
        {
            _context = new ApplicationDbContext();
        }

        public List<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public Payment GetPaymentById(int id)
        {
            return _context.Payments.Find(id);
        }

        public void CreatePayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            _context.Entry(payment).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletePayment(int id)
        {
            var payment = _context.Payments.Find(id);
            _context.Payments.Remove(payment);
            _context.SaveChanges();
        }
    }
}