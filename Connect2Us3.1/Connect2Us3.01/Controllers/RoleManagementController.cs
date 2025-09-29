using Connect2Us3._01.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleManagementController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public RoleManagementController()
        {
        }

        public RoleManagementController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

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

        // GET: RoleManagement
        public ActionResult Index()
        {
            var users = UserManager.Users.ToList();
            var userRoles = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = UserManager.GetRoles(user.Id);
                userRoles.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            ViewBag.AllRoles = new List<string> { "Admin", "Staff", "Customer" };
            return View(userRoles);
        }

        // GET: RoleManagement/AssignRole/5
        public ActionResult AssignRole(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = UserManager.GetRoles(id);
            var availableRoles = new List<string> { "Admin", "Staff", "Customer" };

            var model = new AssignRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CurrentRoles = userRoles.ToList(),
                AvailableRoles = availableRoles.Select(r => new SelectListItem
                {
                    Text = r,
                    Value = r,
                    Selected = userRoles.Contains(r)
                }).ToList()
            };

            return View(model);
        }

        // POST: RoleManagement/AssignRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignRole(AssignRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserManager.FindById(model.UserId);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Remove user from all current roles
            var currentRoles = UserManager.GetRoles(model.UserId);
            if (currentRoles.Any())
            {
                var removeResult = await UserManager.RemoveFromRolesAsync(model.UserId, currentRoles.ToArray());
                if (!removeResult.Succeeded)
                {
                    foreach (var error in removeResult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
            }

            // Add user to selected roles
            if (model.SelectedRoles != null && model.SelectedRoles.Any())
            {
                var addResult = await UserManager.AddToRolesAsync(model.UserId, model.SelectedRoles.ToArray());
                if (!addResult.Succeeded)
                {
                    foreach (var error in addResult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
            }

            TempData["Success"] = "User roles updated successfully.";
            return RedirectToAction("Index");
        }

        // POST: RoleManagement/InitializeRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InitializeRoles()
        {
            var rolesToCreate = new[] { "Admin", "Staff", "Customer" };

            foreach (var roleName in rolesToCreate)
            {
                if (!RoleManager.RoleExists(roleName))
                {
                    var role = new IdentityRole(roleName);
                    await RoleManager.CreateAsync(role);
                }
            }

            TempData["Success"] = "Roles initialized successfully.";
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