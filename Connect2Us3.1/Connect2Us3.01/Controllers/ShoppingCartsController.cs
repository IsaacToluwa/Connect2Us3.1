using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin,Staff,Customer")]
    public class ShoppingCartsController : Controller
    {
        private ShoppingCartBLL _shoppingCartBLL;

        public ShoppingCartsController()
        {
            _shoppingCartBLL = new ShoppingCartBLL();
        }

        // GET: ShoppingCarts
        public ActionResult Index()
        {
            var shoppingCarts = _shoppingCartBLL.GetAllShoppingCarts();
            return View(shoppingCarts);
        }

        // GET: ShoppingCarts/Details/5
        public ActionResult Details(int id)
        {
            var shoppingCart = _shoppingCartBLL.GetShoppingCartById(id);
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCarts/Create
        [HttpPost]
        public ActionResult Create(ShoppingCart shoppingCart)
        {
            try
            {
                _shoppingCartBLL.CreateShoppingCart(shoppingCart);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCarts/Edit/5
        public ActionResult Edit(int id)
        {
            var shoppingCart = _shoppingCartBLL.GetShoppingCartById(id);
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ShoppingCart shoppingCart)
        {
            try
            {
                _shoppingCartBLL.UpdateShoppingCart(shoppingCart);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCarts/Delete/5
        public ActionResult Delete(int id)
        {
            var shoppingCart = _shoppingCartBLL.GetShoppingCartById(id);
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _shoppingCartBLL.DeleteShoppingCart(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}