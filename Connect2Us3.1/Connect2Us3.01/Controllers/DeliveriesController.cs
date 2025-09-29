using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    public class DeliveriesController : Controller
    {
        private DeliveryBLL _deliveryBLL;

        public DeliveriesController()
        {
            _deliveryBLL = new DeliveryBLL();
        }

        // GET: Deliveries
        public ActionResult Index()
        {
            var deliveries = _deliveryBLL.GetAllDeliveries();
            return View(deliveries);
        }

        // GET: Deliveries/Details/5
        public ActionResult Details(int id)
        {
            var delivery = _deliveryBLL.GetDeliveryById(id);
            return View(delivery);
        }

        // GET: Deliveries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deliveries/Create
        [HttpPost]
        public ActionResult Create(Delivery delivery)
        {
            try
            {
                _deliveryBLL.CreateDelivery(delivery);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Deliveries/Edit/5
        public ActionResult Edit(int id)
        {
            var delivery = _deliveryBLL.GetDeliveryById(id);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Delivery delivery)
        {
            try
            {
                _deliveryBLL.UpdateDelivery(delivery);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Deliveries/Delete/5
        public ActionResult Delete(int id)
        {
            var delivery = _deliveryBLL.GetDeliveryById(id);
            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _deliveryBLL.DeleteDelivery(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}