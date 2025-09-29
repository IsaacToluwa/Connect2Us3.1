using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin,Staff,Customer")]
    public class BooksController : Controller
    {
        private BookBLL _bookBLL;

        public BooksController()
        {
            _bookBLL = new BookBLL();
        }

        // GET: Books
        public ActionResult Index()
        {
            var books = _bookBLL.GetAllBooks();
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
            var book = _bookBLL.GetBookById(id);
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Create(Book book)
        {
            try
            {
                _bookBLL.CreateBook(book);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Edit(int id)
        {
            var book = _bookBLL.GetBookById(id);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Edit(int id, Book book)
        {
            try
            {
                _bookBLL.UpdateBook(book);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var book = _bookBLL.GetBookById(id);
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _bookBLL.DeleteBook(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}