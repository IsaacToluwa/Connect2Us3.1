using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connect2Us3._01.Models;

namespace Connect2Us3._01.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Welcome to Connect2Us";
            ViewBag.Message = "Your book connection platform";
            
            // Get some featured books for the landing page
            var featuredBooks = db.Books.Take(6).ToList();
            ViewBag.FeaturedBooks = featuredBooks;
            
            // Get categories for navigation
            var categories = db.Categories.Take(8).ToList();
            ViewBag.Categories = categories;
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Connect2Us - Your book connection platform.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us for any questions or support.";
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}