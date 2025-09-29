using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin,Staff,Customer")]
    public class ReviewsController : Controller
    {
        private ReviewBLL _reviewBLL;

        public ReviewsController()
        {
            _reviewBLL = new ReviewBLL();
        }

        // GET: Reviews
        public ActionResult Index()
        {
            var reviews = _reviewBLL.GetAllReviews();
            return View(reviews);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int id)
        {
            var review = _reviewBLL.GetReviewById(id);
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        public ActionResult Create(Review review)
        {
            try
            {
                _reviewBLL.CreateReview(review);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            var review = _reviewBLL.GetReviewById(id);
            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Review review)
        {
            try
            {
                _reviewBLL.UpdateReview(review);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reviews/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var review = _reviewBLL.GetReviewById(id);
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _reviewBLL.DeleteReview(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}