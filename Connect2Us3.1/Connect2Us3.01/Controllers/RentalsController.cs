using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin,Staff,Customer")]
    public class RentalsController : Controller
    {
        private RentalBLL _rentalBLL;

        public RentalsController()
        {
            _rentalBLL = new RentalBLL();
        }

        // GET: Rentals
        public ActionResult Index()
        {
            var rentals = _rentalBLL.GetAllRentals();
            return View(rentals);
        }

        // GET: Rentals/Details/5
        public ActionResult Details(int id)
        {
            var rental = _rentalBLL.GetRentalById(id);
            return View(rental);
        }

        // GET: Rentals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        public ActionResult Create(Rental rental)
        {
            try
            {
                _rentalBLL.CreateRental(rental);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rentals/Edit/5
        public ActionResult Edit(int id)
        {
            var rental = _rentalBLL.GetRentalById(id);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Rental rental)
        {
            try
            {
                _rentalBLL.UpdateRental(rental);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rentals/Delete/5
        public ActionResult Delete(int id)
        {
            var rental = _rentalBLL.GetRentalById(id);
            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _rentalBLL.DeleteRental(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}