using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    public class AuthorsController : Controller
    {
        private AuthorBLL _authorBLL;

        public AuthorsController()
        {
            _authorBLL = new AuthorBLL();
        }

        // GET: Authors
        public ActionResult Index()
        {
            var authors = _authorBLL.GetAllAuthors();
            return View(authors);
        }

        // GET: Authors/Details/5
        public ActionResult Details(int id)
        {
            var author = _authorBLL.GetAuthorById(id);
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        public ActionResult Create(Author author)
        {
            try
            {
                _authorBLL.CreateAuthor(author);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int id)
        {
            var author = _authorBLL.GetAuthorById(id);
            return View(author);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                _authorBLL.UpdateAuthor(author);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int id)
        {
            var author = _authorBLL.GetAuthorById(id);
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _authorBLL.DeleteAuthor(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}