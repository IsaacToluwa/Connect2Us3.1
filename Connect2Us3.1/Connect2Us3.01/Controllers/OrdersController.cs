using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    public class OrdersController : Controller
    {
        private OrderBLL _orderBLL;

        public OrdersController()
        {
            _orderBLL = new OrderBLL();
        }

        // GET: Orders
        public ActionResult Index()
        {
            var orders = _orderBLL.GetAllOrders();
            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            var order = _orderBLL.GetOrderById(id);
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            try
            {
                _orderBLL.CreateOrder(order);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            var order = _orderBLL.GetOrderById(id);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                _orderBLL.UpdateOrder(order);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            var order = _orderBLL.GetOrderById(id);
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _orderBLL.DeleteOrder(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}