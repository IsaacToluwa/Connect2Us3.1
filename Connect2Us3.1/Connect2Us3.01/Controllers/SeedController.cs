using System;
using System.Linq;
using System.Web.Mvc;
using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Connect2Us3._01.Controllers
{
    public class SeedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public SeedController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SeedDatabase()
        {
            try
            {
                // Check if database exists and create if not
                if (!db.Database.Exists())
                {
                    db.Database.Create();
                }

                // Seed Roles
                SeedRoles();

                // Seed Users
                SeedUsers();

                // Save changes for Categories, Authors, and Books
                SeedCategories();
                db.SaveChanges();

                SeedAuthors();
                db.SaveChanges();

                SeedBooks();
                db.SaveChanges();

                // Final save
                db.SaveChanges();

                ViewBag.Message = "Database seeded successfully!";
                ViewBag.LoginDetails = GetLoginDetails();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error seeding database: " + ex.Message;
            }

            return View("Index");
        }

        private void SeedRoles()
        {
            string[] roleNames = { "Admin", "Staff", "Customer" };

            foreach (string roleName in roleNames)
            {
                if (!roleManager.RoleExists(roleName))
                {
                    var role = new IdentityRole(roleName);
                    roleManager.Create(role);
                }
            }
        }

        private void SeedUsers()
        {
            // Admin User
            if (userManager.FindByEmail("admin@connect2us.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@connect2us.com",
                    Email = "admin@connect2us.com",
                    FirstName = "Admin",
                    LastName = "User",
                    PhoneNumber = "1234567890"
                };

                var result = userManager.Create(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }

            // Staff User
            if (userManager.FindByEmail("staff@connect2us.com") == null)
            {
                var staffUser = new ApplicationUser
                {
                    UserName = "staff@connect2us.com",
                    Email = "staff@connect2us.com",
                    FirstName = "Staff",
                    LastName = "User",
                    PhoneNumber = "1234567891"
                };

                var result = userManager.Create(staffUser, "Staff123!");
                if (result.Succeeded)
                {
                    userManager.AddToRole(staffUser.Id, "Staff");
                }
            }

            // Customer User
            if (userManager.FindByEmail("customer1@connect2us.com") == null)
            {
                var customerUser = new ApplicationUser
                {
                    UserName = "customer1@connect2us.com",
                    Email = "customer1@connect2us.com",
                    FirstName = "Customer",
                    LastName = "One",
                    PhoneNumber = "1234567892"
                };

                var result = userManager.Create(customerUser, "Customer123!");
                if (result.Succeeded)
                {
                    userManager.AddToRole(customerUser.Id, "Customer");
                }
            }
        }

        private void SeedCategories()
        {
            var categories = new[]
            {
                "Fiction", "Non-Fiction", "Mystery", "Romance", "Science Fiction",
                "Fantasy", "Biography", "History", "Self-Help", "Business",
                "Technology", "Health", "Travel", "Cooking", "Art",
                "Music", "Sports", "Children"
            };

            foreach (var categoryName in categories)
            {
                if (!db.Categories.Any(c => c.Name == categoryName))
                {
                    db.Categories.Add(new Category { Name = categoryName });
                }
            }
        }

        private void SeedAuthors()
        {
            var authors = new[]
            {
                "J.K. Rowling", "Stephen King", "Agatha Christie", "Dan Brown", "John Grisham",
                "Nicholas Sparks", "James Patterson", "Danielle Steel", "Nora Roberts", "Tom Clancy",
                "Michael Crichton", "Dean Koontz", "Patricia Cornwell", "John le Carré", "Ken Follett",
                "Jeffrey Archer", "Sidney Sheldon", "Jackie Collins"
            };

            foreach (var authorName in authors)
            {
                if (!db.Authors.Any(a => a.Name == authorName))
                {
                    db.Authors.Add(new Author { Name = authorName });
                }
            }
        }

        private void SeedBooks()
        {
            var books = new[]
            {
                new { Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", Category = "Fantasy", Price = 15.99m },
                new { Title = "The Shining", Author = "Stephen King", Category = "Fiction", Price = 12.99m },
                new { Title = "Murder on the Orient Express", Author = "Agatha Christie", Category = "Mystery", Price = 10.99m },
                new { Title = "The Da Vinci Code", Author = "Dan Brown", Category = "Mystery", Price = 14.99m },
                new { Title = "The Firm", Author = "John Grisham", Category = "Fiction", Price = 13.99m },
                new { Title = "The Notebook", Author = "Nicholas Sparks", Category = "Romance", Price = 11.99m },
                new { Title = "Along Came a Spider", Author = "James Patterson", Category = "Mystery", Price = 12.99m },
                new { Title = "The Gift", Author = "Danielle Steel", Category = "Romance", Price = 10.99m }
            };

            foreach (var book in books)
            {
                if (!db.Books.Any(b => b.Title == book.Title))
                {
                    var author = db.Authors.FirstOrDefault(a => a.Name == book.Author);
                    var category = db.Categories.FirstOrDefault(c => c.Name == book.Category);

                    if (author != null && category != null)
                    {
                        db.Books.Add(new Book
                        {
                            Title = book.Title,
                            AuthorId = author.AuthorId,
                            CategoryId = category.CategoryId,
                            Price = book.Price,
                            StockQuantity = 10,
                            Description = $"A great book by {book.Author}"
                        });
                    }
                }
            }
        }

        private string GetLoginDetails()
        {
            return @"
                <h4>Login Credentials:</h4>
                <ul>
                    <li><strong>Admin:</strong> admin@connect2us.com / Admin123!</li>
                    <li><strong>Staff:</strong> staff@connect2us.com / Staff123!</li>
                    <li><strong>Customer:</strong> customer1@connect2us.com / Customer123!</li>
                </ul>";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                userManager.Dispose();
                roleManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}