using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    public class WishlistsController : Controller
    {
        private WishlistBLL _wishlistBLL;

        public WishlistsController()
        {
            _wishlistBLL = new WishlistBLL();
        }

        // GET: Wishlists
        public ActionResult Index()
        {
            var wishlists = _wishlistBLL.GetAllWishlists();
            return View(wishlists);
        }

        // GET: Wishlists/Details/5
        public ActionResult Details(int id)
        {
            var wishlist = _wishlistBLL.GetWishlistById(id);
            return View(wishlist);
        }

        // GET: Wishlists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wishlists/Create
        [HttpPost]
        public ActionResult Create(Wishlist wishlist)
        {
            try
            {
                _wishlistBLL.CreateWishlist(wishlist);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Wishlists/Edit/5
        public ActionResult Edit(int id)
        {
            var wishlist = _wishlistBLL.GetWishlistById(id);
            return View(wishlist);
        }

        // POST: Wishlists/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Wishlist wishlist)
        {
            try
            {
                _wishlistBLL.UpdateWishlist(wishlist);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Wishlists/Delete/5
        public ActionResult Delete(int id)
        {
            var wishlist = _wishlistBLL.GetWishlistById(id);
            return View(wishlist);
        }

        // POST: Wishlists/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _wishlistBLL.DeleteWishlist(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}