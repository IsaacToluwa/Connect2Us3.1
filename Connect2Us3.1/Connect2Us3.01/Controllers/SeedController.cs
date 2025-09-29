using System;
using System.Linq;
using System.Web.Mvc;
using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Connect2Us3._01.Controllers
{
    [Authorize(Roles = "Admin")]
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
                // Seed Roles
                SeedRoles();
                
                // Seed Users
                SeedUsers();
                
                // Seed Categories
                SeedCategories();
                
                // Seed Authors
                SeedAuthors();
                
                // Seed Books
                SeedBooks();
                
                // Save all changes
                db.SaveChanges();
                
                ViewBag.Message = "Database seeded successfully!";
                ViewBag.LoginDetails = GetLoginDetails();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error seeding database: {ex.Message}";
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
            // Create Admin User
            if (userManager.FindByEmail("admin@connect2us.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@connect2us.com",
                    Email = "admin@connect2us.com",
                    FirstName = "Admin",
                    LastName = "User",
                    PhoneNumber = "1234567890",
                    Address = "123 Admin Street"
                };
                
                var result = userManager.Create(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }

            // Create Staff User
            if (userManager.FindByEmail("staff@connect2us.com") == null)
            {
                var staffUser = new ApplicationUser
                {
                    UserName = "staff@connect2us.com",
                    Email = "staff@connect2us.com",
                    FirstName = "Staff",
                    LastName = "Member",
                    PhoneNumber = "1234567891",
                    Address = "456 Staff Avenue"
                };
                
                var result = userManager.Create(staffUser, "Staff123!");
                if (result.Succeeded)
                {
                    userManager.AddToRole(staffUser.Id, "Staff");
                }
            }

            // Create Customer Users
            for (int i = 1; i <= 13; i++)
            {
                string email = $"customer{i}@connect2us.com";
                if (userManager.FindByEmail(email) == null)
                {
                    var customerUser = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = $"Customer{i}",
                        LastName = "User",
                        PhoneNumber = $"123456789{i:D2}",
                        Address = $"{i * 100} Customer Street"
                    };
                    
                    var result = userManager.Create(customerUser, "Customer123!");
                    if (result.Succeeded)
                    {
                        userManager.AddToRole(customerUser.Id, "Customer");
                    }
                }
            }
        }

        private void SeedCategories()
        {
            if (!db.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Fiction", Description = "Fictional stories and novels" },
                    new Category { Name = "Non-Fiction", Description = "Real-life stories and factual content" },
                    new Category { Name = "Science Fiction", Description = "Futuristic and scientific fiction" },
                    new Category { Name = "Mystery", Description = "Mystery and thriller novels" },
                    new Category { Name = "Romance", Description = "Romantic novels and stories" },
                    new Category { Name = "Biography", Description = "Life stories of famous people" },
                    new Category { Name = "History", Description = "Historical books and documentaries" },
                    new Category { Name = "Science", Description = "Scientific research and discoveries" },
                    new Category { Name = "Technology", Description = "Technology and programming books" },
                    new Category { Name = "Business", Description = "Business and entrepreneurship" },
                    new Category { Name = "Self-Help", Description = "Personal development and motivation" },
                    new Category { Name = "Health", Description = "Health and wellness guides" },
                    new Category { Name = "Travel", Description = "Travel guides and experiences" },
                    new Category { Name = "Cooking", Description = "Recipes and culinary arts" },
                    new Category { Name = "Art", Description = "Art history and techniques" },
                    new Category { Name = "Philosophy", Description = "Philosophical thoughts and theories" },
                    new Category { Name = "Religion", Description = "Religious texts and spiritual guidance" },
                    new Category { Name = "Education", Description = "Educational materials and textbooks" }
                };

                foreach (var category in categories)
                {
                    db.Categories.Add(category);
                }
            }
        }

        private void SeedAuthors()
        {
            if (!db.Authors.Any())
            {
                var authors = new[]
                {
                    new Author { FirstName = "J.K.", LastName = "Rowling", Biography = "British author, best known for Harry Potter series" },
                    new Author { FirstName = "Stephen", LastName = "King", Biography = "American author of horror, supernatural fiction" },
                    new Author { FirstName = "Agatha", LastName = "Christie", Biography = "English writer known for detective novels" },
                    new Author { FirstName = "George", LastName = "Orwell", Biography = "English novelist and essayist" },
                    new Author { FirstName = "Jane", LastName = "Austen", Biography = "English novelist known for romantic fiction" },
                    new Author { FirstName = "Mark", LastName = "Twain", Biography = "American writer and humorist" },
                    new Author { FirstName = "Charles", LastName = "Dickens", Biography = "English writer and social critic" },
                    new Author { FirstName = "Ernest", LastName = "Hemingway", Biography = "American novelist and journalist" },
                    new Author { FirstName = "William", LastName = "Shakespeare", Biography = "English playwright and poet" },
                    new Author { FirstName = "Leo", LastName = "Tolstoy", Biography = "Russian writer regarded as one of the greatest authors" },
                    new Author { FirstName = "Harper", LastName = "Lee", Biography = "American novelist known for To Kill a Mockingbird" },
                    new Author { FirstName = "F. Scott", LastName = "Fitzgerald", Biography = "American novelist of the Jazz Age" },
                    new Author { FirstName = "Virginia", LastName = "Woolf", Biography = "English writer and modernist" },
                    new Author { FirstName = "Gabriel", LastName = "García Márquez", Biography = "Colombian novelist and Nobel Prize winner" },
                    new Author { FirstName = "Toni", LastName = "Morrison", Biography = "American novelist and Nobel Prize winner" },
                    new Author { FirstName = "Maya", LastName = "Angelou", Biography = "American poet and civil rights activist" },
                    new Author { FirstName = "Dan", LastName = "Brown", Biography = "American author of thriller fiction" },
                    new Author { FirstName = "Paulo", LastName = "Coelho", Biography = "Brazilian lyricist and novelist" }
                };

                foreach (var author in authors)
                {
                    db.Authors.Add(author);
                }
            }
        }

        private void SeedBooks()
        {
            if (!db.Books.Any())
            {
                // Get categories and authors for reference
                var categories = db.Categories.ToList();
                var authors = db.Authors.ToList();

                var books = new[]
                {
                    new Book { Title = "Harry Potter and the Philosopher's Stone", ISBN = "9780747532699", Price = 15.99m, Stock = 50, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Rowling").AuthorId },
                    new Book { Title = "The Shining", ISBN = "9780307743657", Price = 18.99m, Stock = 30, CategoryId = categories.First(c => c.Name == "Mystery").CategoryId, AuthorId = authors.First(a => a.LastName == "King").AuthorId },
                    new Book { Title = "Murder on the Orient Express", ISBN = "9780062693662", Price = 14.99m, Stock = 25, CategoryId = categories.First(c => c.Name == "Mystery").CategoryId, AuthorId = authors.First(a => a.LastName == "Christie").AuthorId },
                    new Book { Title = "1984", ISBN = "9780451524935", Price = 13.99m, Stock = 40, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Orwell").AuthorId },
                    new Book { Title = "Pride and Prejudice", ISBN = "9780141439518", Price = 12.99m, Stock = 35, CategoryId = categories.First(c => c.Name == "Romance").CategoryId, AuthorId = authors.First(a => a.LastName == "Austen").AuthorId },
                    new Book { Title = "The Adventures of Tom Sawyer", ISBN = "9780486400778", Price = 11.99m, Stock = 20, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Twain").AuthorId },
                    new Book { Title = "A Tale of Two Cities", ISBN = "9780486406510", Price = 10.99m, Stock = 28, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Dickens").AuthorId },
                    new Book { Title = "The Old Man and the Sea", ISBN = "9780684801223", Price = 16.99m, Stock = 22, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Hemingway").AuthorId },
                    new Book { Title = "Romeo and Juliet", ISBN = "9780486275437", Price = 8.99m, Stock = 45, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Shakespeare").AuthorId },
                    new Book { Title = "War and Peace", ISBN = "9780199232765", Price = 24.99m, Stock = 15, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Tolstoy").AuthorId },
                    new Book { Title = "To Kill a Mockingbird", ISBN = "9780061120084", Price = 17.99m, Stock = 33, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Lee").AuthorId },
                    new Book { Title = "The Great Gatsby", ISBN = "9780743273565", Price = 15.99m, Stock = 38, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Fitzgerald").AuthorId },
                    new Book { Title = "Mrs. Dalloway", ISBN = "9780156628709", Price = 14.99m, Stock = 18, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Woolf").AuthorId },
                    new Book { Title = "One Hundred Years of Solitude", ISBN = "9780060883287", Price = 19.99m, Stock = 26, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "García Márquez").AuthorId },
                    new Book { Title = "Beloved", ISBN = "9781400033416", Price = 16.99m, Stock = 21, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Morrison").AuthorId },
                    new Book { Title = "I Know Why the Caged Bird Sings", ISBN = "9780345514400", Price = 15.99m, Stock = 29, CategoryId = categories.First(c => c.Name == "Biography").CategoryId, AuthorId = authors.First(a => a.LastName == "Angelou").AuthorId },
                    new Book { Title = "The Da Vinci Code", ISBN = "9780307474278", Price = 18.99m, Stock = 42, CategoryId = categories.First(c => c.Name == "Mystery").CategoryId, AuthorId = authors.First(a => a.LastName == "Brown").AuthorId },
                    new Book { Title = "The Alchemist", ISBN = "9780061122415", Price = 13.99m, Stock = 55, CategoryId = categories.First(c => c.Name == "Fiction").CategoryId, AuthorId = authors.First(a => a.LastName == "Coelho").AuthorId }
                };

                foreach (var book in books)
                {
                    db.Books.Add(book);
                }
            }
        }

        private string GetLoginDetails()
        {
            return @"
                <h4>Login Details:</h4>
                <ul>
                    <li><strong>Admin:</strong> admin@connect2us.com / Admin123!</li>
                    <li><strong>Staff:</strong> staff@connect2us.com / Staff123!</li>
                    <li><strong>Customers:</strong> customer1@connect2us.com to customer13@connect2us.com / Customer123!</li>
                </ul>";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                userManager?.Dispose();
                roleManager?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}