using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using System.Collections.Generic;
using System.Linq;

namespace Connect2Us3._01.BLL
{
    public class OrderDetailBLL
    {
        private ApplicationDbContext _context;

        public OrderDetailBLL()
        {
            _context = new ApplicationDbContext();
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            return _context.OrderDetails.ToList();
        }

        public OrderDetail GetOrderDetailById(int id)
        {
            return _context.OrderDetails.Find(id);
        }

        public void CreateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.Entry(orderDetail).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteOrderDetail(int id)
        {
            var orderDetail = _context.OrderDetails.Find(id);
            _context.OrderDetails.Remove(orderDetail);
            _context.SaveChanges();
        }
    }
}