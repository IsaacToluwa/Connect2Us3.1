using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin,Staff,Customer")]
    public class ReservationsController : Controller
    {
        private ReservationBLL _reservationBLL;

        public ReservationsController()
        {
            _reservationBLL = new ReservationBLL();
        }

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = _reservationBLL.GetAllReservations();
            return View(reservations);
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int id)
        {
            var reservation = _reservationBLL.GetReservationById(id);
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            try
            {
                _reservationBLL.CreateReservation(reservation);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int id)
        {
            var reservation = _reservationBLL.GetReservationById(id);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Reservation reservation)
        {
            try
            {
                _reservationBLL.UpdateReservation(reservation);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int id)
        {
            var reservation = _reservationBLL.GetReservationById(id);
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _reservationBLL.DeleteReservation(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}