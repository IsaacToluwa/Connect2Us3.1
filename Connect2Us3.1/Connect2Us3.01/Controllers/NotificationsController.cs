using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin,Staff,Customer")]
    public class NotificationsController : Controller
    {
        private NotificationBLL _notificationBLL;

        public NotificationsController()
        {
            _notificationBLL = new NotificationBLL();
        }

        // GET: Notifications
        public ActionResult Index()
        {
            var notifications = _notificationBLL.GetAllNotifications();
            return View(notifications);
        }

        // GET: Notifications/Details/5
        public ActionResult Details(int id)
        {
            var notification = _notificationBLL.GetNotificationById(id);
            return View(notification);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Create
        [HttpPost]
        public ActionResult Create(Notification notification)
        {
            try
            {
                _notificationBLL.CreateNotification(notification);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int id)
        {
            var notification = _notificationBLL.GetNotificationById(id);
            return View(notification);
        }

        // POST: Notifications/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Notification notification)
        {
            try
            {
                _notificationBLL.UpdateNotification(notification);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int id)
        {
            var notification = _notificationBLL.GetNotificationById(id);
            return View(notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _notificationBLL.DeleteNotification(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}