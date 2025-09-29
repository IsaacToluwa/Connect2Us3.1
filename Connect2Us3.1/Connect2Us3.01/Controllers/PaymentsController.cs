using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class PaymentsController : Controller
    {
        private PaymentBLL _paymentBLL;

        public PaymentsController()
        {
            _paymentBLL = new PaymentBLL();
        }

        // GET: Payments
        public ActionResult Index()
        {
            var payments = _paymentBLL.GetAllPayments();
            return View(payments);
        }

        // GET: Payments/Details/5
        public ActionResult Details(int id)
        {
            var payment = _paymentBLL.GetPaymentById(id);
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            try
            {
                _paymentBLL.CreatePayment(payment);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int id)
        {
            var payment = _paymentBLL.GetPaymentById(id);
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Payment payment)
        {
            try
            {
                _paymentBLL.UpdatePayment(payment);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int id)
        {
            var payment = _paymentBLL.GetPaymentById(id);
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _paymentBLL.DeletePayment(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}