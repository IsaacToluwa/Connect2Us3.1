using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class OrderDetailsController : Controller
    {
        private OrderDetailBLL _orderDetailBLL;

        public OrderDetailsController()
        {
            _orderDetailBLL = new OrderDetailBLL();
        }

        // GET: OrderDetails
        public ActionResult Index()
        {
            var orderDetails = _orderDetailBLL.GetAllOrderDetails();
            return View(orderDetails);
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int id)
        {
            var orderDetail = _orderDetailBLL.GetOrderDetailById(id);
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderDetails/Create
        [HttpPost]
        public ActionResult Create(OrderDetail orderDetail)
        {
            try
            {
                _orderDetailBLL.CreateOrderDetail(orderDetail);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int id)
        {
            var orderDetail = _orderDetailBLL.GetOrderDetailById(id);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, OrderDetail orderDetail)
        {
            try
            {
                _orderDetailBLL.UpdateOrderDetail(orderDetail);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int id)
        {
            var orderDetail = _orderDetailBLL.GetOrderDetailById(id);
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _orderDetailBLL.DeleteOrderDetail(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}