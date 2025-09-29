using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Connect2Us3._01.Models;
using System.Threading.Tasks;
using System.Web;

namespace Connect2Us3._01.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Test
        public ActionResult Index()
        {
            ViewBag.CurrentUser = User.Identity.Name;
            ViewBag.IsAdmin = User.IsInRole("Admin");
            ViewBag.IsStaff = User.IsInRole("Staff");
            ViewBag.IsCustomer = User.IsInRole("Customer");
            
            var userId = User.Identity.GetUserId();
            var userRoles = UserManager.GetRoles(userId);
            ViewBag.UserRoles = userRoles;
            
            return View();
        }

        // GET: Test/AdminOnly - Only Admin can access
        [Authorize(Roles = "Admin")]
        public ActionResult AdminOnly()
        {
            ViewBag.Message = "This page is only accessible by Admin users.";
            return View();
        }

        // GET: Test/StaffOnly - Only Staff can access
        [Authorize(Roles = "Staff")]
        public ActionResult StaffOnly()
        {
            ViewBag.Message = "This page is only accessible by Staff users.";
            return View();
        }

        // GET: Test/CustomerOnly - Only Customer can access
        [Authorize(Roles = "Customer")]
        public ActionResult CustomerOnly()
        {
            ViewBag.Message = "This page is only accessible by Customer users.";
            return View();
        }

        // GET: Test/AdminOrStaff - Admin or Staff can access
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult AdminOrStaff()
        {
            ViewBag.Message = "This page is accessible by Admin or Staff users.";
            return View();
        }

        // POST: Test/CreateTestUser - Create a test user with specific role
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateTestUser(string username, string email, string password, string role)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                TempData["Error"] = "All fields are required.";
                return RedirectToAction("Index");
            }

            var user = new ApplicationUser { UserName = username, Email = email };
            var result = await UserManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Ensure role exists
                if (!RoleManager.RoleExists(role))
                {
                    await RoleManager.CreateAsync(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(role));
                }

                // Add user to role
                await UserManager.AddToRoleAsync(user.Id, role);
                TempData["Success"] = $"Test user '{username}' created successfully with role '{role}'.";
            }
            else
            {
                TempData["Error"] = "Failed to create user: " + string.Join(", ", result.Errors);
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                    _roleManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}