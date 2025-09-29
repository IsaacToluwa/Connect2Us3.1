using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using System.Collections.Generic;
using System.Linq;

namespace Connect2Us3._01.BLL
{
    public class NotificationBLL
    {
        private ApplicationDbContext _context;

        public NotificationBLL()
        {
            _context = new ApplicationDbContext();
        }

        public List<Notification> GetAllNotifications()
        {
            return _context.Notifications.ToList();
        }

        public Notification GetNotificationById(int id)
        {
            return _context.Notifications.Find(id);
        }

        public void CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public void UpdateNotification(Notification notification)
        {
            _context.Entry(notification).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteNotification(int id)
        {
            var notification = _context.Notifications.Find(id);
            _context.Notifications.Remove(notification);
            _context.SaveChanges();
        }
    }
}