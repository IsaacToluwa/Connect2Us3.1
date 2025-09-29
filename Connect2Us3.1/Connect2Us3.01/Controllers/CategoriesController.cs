using Connect2Us3._01.BLL;
using Connect2Us3._01.Models;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin,Staff,Customer")]
    public class CategoriesController : Controller
    {
        private CategoryBLL _categoryBLL;

        public CategoriesController()
        {
            _categoryBLL = new CategoryBLL();
        }

        // GET: Categories
        public ActionResult Index()
        {
            var categories = _categoryBLL.GetAllCategories();
            return View(categories);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            var category = _categoryBLL.GetCategoryById(id);
            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryBLL.CreateCategory(category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Edit(int id)
        {
            var category = _categoryBLL.GetCategoryById(id);
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                _categoryBLL.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var category = _categoryBLL.GetCategoryById(id);
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _categoryBLL.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}