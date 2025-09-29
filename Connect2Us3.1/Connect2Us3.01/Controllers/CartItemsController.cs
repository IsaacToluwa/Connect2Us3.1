using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    public class CartItemsController : Controller
    {
        private CartItemBLL _cartItemBLL;

        public CartItemsController()
        {
            _cartItemBLL = new CartItemBLL();
        }

        // GET: CartItems
        public ActionResult Index()
        {
            var cartItems = _cartItemBLL.GetAllCartItems();
            return View(cartItems);
        }

        // GET: CartItems/Details/5
        public ActionResult Details(int id)
        {
            var cartItem = _cartItemBLL.GetCartItemById(id);
            return View(cartItem);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartItems/Create
        [HttpPost]
        public ActionResult Create(CartItem cartItem)
        {
            try
            {
                _cartItemBLL.CreateCartItem(cartItem);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int id)
        {
            var cartItem = _cartItemBLL.GetCartItemById(id);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CartItem cartItem)
        {
            try
            {
                _cartItemBLL.UpdateCartItem(cartItem);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int id)
        {
            var cartItem = _cartItemBLL.GetCartItemById(id);
            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _cartItemBLL.DeleteCartItem(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}