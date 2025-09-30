namespace Connect2Us3._01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Connect2Us3._01.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<Connect2Us3._01.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Connect2Us3._01.DAL.ApplicationDbContext";
        }

        protected override void Seed(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            // Seed Roles
            SeedRoles(context);
            
            // Seed Users
            SeedUsers(context);
            
            // Seed Categories
            SeedCategories(context);
            
            // Seed Authors
            SeedAuthors(context);
            
            // Seed Books
            SeedBooks(context);
            
            // Save changes after seeding core data
            context.SaveChanges();
            
            // Seed dependent data (requires existing users and books)
            SeedOrders(context);
            SeedOrderDetails(context);
            SeedRentals(context);
            SeedReservations(context);
            SeedDeliveries(context);
            SeedPayments(context);
            SeedReviews(context);
            SeedWishlists(context);
            SeedNotifications(context);
            SeedShoppingCarts(context);
            SeedCartItems(context);
            
            context.SaveChanges();
        }

        public void PublicSeed(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            Seed(context);
        }

        private void SeedRoles(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roleNames = { "Admin", "Staff", "Customer" };
            foreach (var roleName in roleNames)
            {
                if (!roleManager.RoleExists(roleName))
                {
                    roleManager.Create(new IdentityRole(roleName));
                }
            }
        }

        private void SeedUsers(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            // Admin User
            if (userManager.FindByEmail("admin@connect2us.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@connect2us.com",
                    Email = "admin@connect2us.com",
                    FirstName = "Admin",
                    LastName = "User",
                    PhoneNumber = "1234567890",
                    Address = "123 Admin Street",
                    City = "Admin City",
                    State = "Admin State",
                    ZipCode = "12345"
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
                    LastName = "Member",
                    PhoneNumber = "2345678901",
                    Address = "456 Staff Avenue",
                    City = "Staff City",
                    State = "Staff State",
                    ZipCode = "23456"
                };
                
                var result = userManager.Create(staffUser, "Staff123!");
                if (result.Succeeded)
                {
                    userManager.AddToRole(staffUser.Id, "Staff");
                }
            }
            
            // Customer User
            if (userManager.FindByEmail("customer@connect2us.com") == null)
            {
                var customerUser = new ApplicationUser
                {
                    UserName = "customer@connect2us.com",
                    Email = "customer@connect2us.com",
                    FirstName = "John",
                    LastName = "Customer",
                    PhoneNumber = "3456789012",
                    Address = "789 Customer Lane",
                    City = "Customer City",
                    State = "Customer State",
                    ZipCode = "34567"
                };
                
                var result = userManager.Create(customerUser, "Customer123!");
                if (result.Succeeded)
                {
                    userManager.AddToRole(customerUser.Id, "Customer");
                }
            }
            
            // Additional Customer Users (12 more)
            for (int i = 1; i <= 12; i++)
            {
                string email = $"customer{i}@connect2us.com";
                if (userManager.FindByEmail(email) == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = $"Customer{i}",
                        LastName = "User",
                        PhoneNumber = $"555000{i:D4}",
                        Address = $"{i * 100} Customer Street",
                        City = "Customer City",
                        State = "Customer State",
                        ZipCode = $"{10000 + i}"
                    };
                    
                    var result = userManager.Create(user, "Customer123!");
                    if (result.Succeeded)
                    {
                        userManager.AddToRole(user.Id, "Customer");
                    }
                }
            }
        }

        private void SeedCategories(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            context.Categories.AddOrUpdate(c => c.Name,
                new Category { Name = "Fiction" },
                new Category { Name = "Non-Fiction" },
                new Category { Name = "Science Fiction" },
                new Category { Name = "Mystery" },
                new Category { Name = "Romance" },
                new Category { Name = "Biography" },
                new Category { Name = "History" },
                new Category { Name = "Science" },
                new Category { Name = "Technology" },
                new Category { Name = "Business" },
                new Category { Name = "Self-Help" },
                new Category { Name = "Health" },
                new Category { Name = "Travel" },
                new Category { Name = "Cooking" },
                new Category { Name = "Art" },
                new Category { Name = "Philosophy" },
                new Category { Name = "Religion" },
                new Category { Name = "Education" }
            );
        }

        private void SeedAuthors(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            context.Authors.AddOrUpdate(a => a.Name,
                new Author { Name = "J.K. Rowling" },
                new Author { Name = "Stephen King" },
                new Author { Name = "Agatha Christie" },
                new Author { Name = "George Orwell" },
                new Author { Name = "Jane Austen" },
                new Author { Name = "Mark Twain" },
                new Author { Name = "Ernest Hemingway" },
                new Author { Name = "William Shakespeare" },
                new Author { Name = "Charles Dickens" },
                new Author { Name = "Leo Tolstoy" },
                new Author { Name = "F. Scott Fitzgerald" },
                new Author { Name = "Harper Lee" },
                new Author { Name = "Gabriel García Márquez" },
                new Author { Name = "Toni Morrison" },
                new Author { Name = "Maya Angelou" },
                new Author { Name = "Dan Brown" },
                new Author { Name = "John Grisham" },
                new Author { Name = "Paulo Coelho" }
            );
        }

        private void SeedBooks(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            // Get categories and authors for foreign key references
            var fiction = context.Categories.FirstOrDefault(c => c.Name == "Fiction");
            var mystery = context.Categories.FirstOrDefault(c => c.Name == "Mystery");
            var sciFi = context.Categories.FirstOrDefault(c => c.Name == "Science Fiction");
            var romance = context.Categories.FirstOrDefault(c => c.Name == "Romance");
            var biography = context.Categories.FirstOrDefault(c => c.Name == "Biography");
            
            var rowling = context.Authors.FirstOrDefault(a => a.Name == "J.K. Rowling");
            var king = context.Authors.FirstOrDefault(a => a.Name == "Stephen King");
            var christie = context.Authors.FirstOrDefault(a => a.Name == "Agatha Christie");
            var orwell = context.Authors.FirstOrDefault(a => a.Name == "George Orwell");
            var austen = context.Authors.FirstOrDefault(a => a.Name == "Jane Austen");

            context.Books.AddOrUpdate(b => b.Title,
                new Book { Title = "Harry Potter and the Philosopher's Stone", ISBN = "9780747532699", Price = 15.99m, Stock = 25, CategoryId = fiction?.CategoryId ?? 1, AuthorId = rowling?.AuthorId ?? 1, PublicationDate = new DateTime(1997, 6, 26), Description = "The first book in the Harry Potter series", IsRentable = true, RentalPrice = 3.99m },
                new Book { Title = "The Shining", ISBN = "9780307743657", Price = 12.99m, Stock = 20, CategoryId = fiction?.CategoryId ?? 1, AuthorId = king?.AuthorId ?? 2, PublicationDate = new DateTime(1977, 1, 28), Description = "A horror novel by Stephen King", IsRentable = true, RentalPrice = 2.99m },
                new Book { Title = "Murder on the Orient Express", ISBN = "9780062693662", Price = 13.99m, Stock = 18, CategoryId = mystery?.CategoryId ?? 4, AuthorId = christie?.AuthorId ?? 3, PublicationDate = new DateTime(1934, 1, 1), Description = "A detective novel featuring Hercule Poirot", IsRentable = false, RentalPrice = null },
                new Book { Title = "1984", ISBN = "9780451524935", Price = 14.99m, Stock = 30, CategoryId = sciFi?.CategoryId ?? 3, AuthorId = orwell?.AuthorId ?? 4, PublicationDate = new DateTime(1949, 6, 8), Description = "A dystopian social science fiction novel", IsRentable = true, RentalPrice = 4.49m },
                new Book { Title = "Pride and Prejudice", ISBN = "9780141439518", Price = 11.99m, Stock = 22, CategoryId = romance?.CategoryId ?? 5, AuthorId = austen?.AuthorId ?? 5, PublicationDate = new DateTime(1813, 1, 28), Description = "A romantic novel by Jane Austen", IsRentable = true, RentalPrice = 2.49m },
                new Book { Title = "Harry Potter and the Chamber of Secrets", ISBN = "9780747538493", Price = 15.99m, Stock = 23, CategoryId = fiction?.CategoryId ?? 1, AuthorId = rowling?.AuthorId ?? 1, PublicationDate = new DateTime(1998, 7, 2), Description = "The second book in the Harry Potter series", IsRentable = true, RentalPrice = 3.99m },
                new Book { Title = "It", ISBN = "9781501142970", Price = 16.99m, Stock = 15, CategoryId = fiction?.CategoryId ?? 1, AuthorId = king?.AuthorId ?? 2, PublicationDate = new DateTime(1986, 9, 15), Description = "A horror novel about a shape-shifting entity", IsRentable = false, RentalPrice = null },
                new Book { Title = "And Then There Were None", ISBN = "9780062073488", Price = 12.99m, Stock = 19, CategoryId = mystery?.CategoryId ?? 4, AuthorId = christie?.AuthorId ?? 3, PublicationDate = new DateTime(1939, 11, 6), Description = "A mystery novel about ten strangers on an island", IsRentable = true, RentalPrice = 3.29m },
                new Book { Title = "Animal Farm", ISBN = "9780451526342", Price = 10.99m, Stock = 28, CategoryId = fiction?.CategoryId ?? 1, AuthorId = orwell?.AuthorId ?? 4, PublicationDate = new DateTime(1945, 8, 17), Description = "An allegorical novella about farm animals", IsRentable = true, RentalPrice = 2.19m },
                new Book { Title = "Sense and Sensibility", ISBN = "9780141439662", Price = 11.99m, Stock = 21, CategoryId = romance?.CategoryId ?? 5, AuthorId = austen?.AuthorId ?? 5, PublicationDate = new DateTime(1811, 10, 30), Description = "Jane Austen's first published novel", IsRentable = true, RentalPrice = 2.49m },
                new Book { Title = "Harry Potter and the Prisoner of Azkaban", ISBN = "9780747542155", Price = 15.99m, Stock = 24, CategoryId = fiction?.CategoryId ?? 1, AuthorId = rowling?.AuthorId ?? 1, PublicationDate = new DateTime(1999, 7, 8), Description = "The third book in the Harry Potter series", IsRentable = true, RentalPrice = 3.99m },
                new Book { Title = "The Stand", ISBN = "9780307743688", Price = 18.99m, Stock = 12, CategoryId = fiction?.CategoryId ?? 1, AuthorId = king?.AuthorId ?? 2, PublicationDate = new DateTime(1978, 10, 3), Description = "A post-apocalyptic dark fantasy novel", IsRentable = false, RentalPrice = null },
                new Book { Title = "The ABC Murders", ISBN = "9780062073556", Price = 13.99m, Stock = 17, CategoryId = mystery?.CategoryId ?? 4, AuthorId = christie?.AuthorId ?? 3, PublicationDate = new DateTime(1936, 1, 6), Description = "A detective novel featuring Hercule Poirot", IsRentable = true, RentalPrice = 3.29m },
                new Book { Title = "Homage to Catalonia", ISBN = "9780156421171", Price = 13.99m, Stock = 16, CategoryId = biography?.CategoryId ?? 6, AuthorId = orwell?.AuthorId ?? 4, PublicationDate = new DateTime(1938, 4, 25), Description = "Orwell's account of the Spanish Civil War", IsRentable = true, RentalPrice = 3.49m },
                new Book { Title = "Emma", ISBN = "9780141439587", Price = 11.99m, Stock = 20, CategoryId = romance?.CategoryId ?? 5, AuthorId = austen?.AuthorId ?? 5, PublicationDate = new DateTime(1815, 12, 23), Description = "A novel about a young woman's misguided matchmaking", IsRentable = true, RentalPrice = 2.49m },
                new Book { Title = "Harry Potter and the Goblet of Fire", ISBN = "9780747546245", Price = 16.99m, Stock = 22, CategoryId = fiction?.CategoryId ?? 1, AuthorId = rowling?.AuthorId ?? 1, PublicationDate = new DateTime(2000, 7, 8), Description = "The fourth book in the Harry Potter series", IsRentable = true, RentalPrice = 4.99m }
            );
        }

        private void SeedOrders(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var customers = context.Users.Where(u => u.Roles.Any(r => r.RoleId == context.Roles.FirstOrDefault(role => role.Name == "Customer").Id)).Take(15).ToList();
            
            for (int i = 0; i < 15 && i < customers.Count; i++)
            {
                context.Orders.AddOrUpdate(o => o.OrderId,
                    new Order
                    {
                        OrderId = i + 1,
                        UserId = customers[i].Id,
                        OrderDate = DateTime.Now.AddDays(-30 + i * 2),
                        TotalAmount = 25.99m + (i * 5),
                        Status = i % 3 == 0 ? "Completed" : (i % 3 == 1 ? "Processing" : "Shipped"),
                        ShippingAddress = $"{100 + i * 10} Order Street, City {i + 1}, State, {10000 + i}"
                    }
                );
            }
        }

        private void SeedOrderDetails(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var books = context.Books.Take(15).ToList();
            
            for (int i = 0; i < 15 && i < books.Count; i++)
            {
                context.OrderDetails.AddOrUpdate(od => od.OrderDetailId,
                    new OrderDetail
                    {
                        OrderDetailId = i + 1,
                        OrderId = (i % 15) + 1,
                        BookId = books[i].BookId,
                        Quantity = (i % 3) + 1,
                        UnitPrice = books[i].Price
                    }
                );
            }
        }

        private void SeedRentals(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var customers = context.Users.Where(u => u.Roles.Any(r => r.RoleId == context.Roles.FirstOrDefault(role => role.Name == "Customer").Id)).Take(15).ToList();
            var books = context.Books.Take(15).ToList();
            
            for (int i = 0; i < 15 && i < customers.Count && i < books.Count; i++)
            {
                context.Rentals.AddOrUpdate(r => r.RentalId,
                    new Rental
                    {
                        RentalId = i + 1,
                        UserId = customers[i].Id,
                        BookId = books[i].BookId,
                        RentalDate = DateTime.Now.AddDays(-20 + i),
                        DueDate = DateTime.Now.AddDays(-6 + i),
                        ReturnDate = i % 2 == 0 ? DateTime.Now.AddDays(-5 + i) : (DateTime?)null,
                        RentalFee = 5.99m + (i * 0.5m),
                        Status = i % 2 == 0 ? "Returned" : "Active"
                    }
                );
            }
        }

        private void SeedReservations(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var customers = context.Users.Where(u => u.Roles.Any(r => r.RoleId == context.Roles.FirstOrDefault(role => role.Name == "Customer").Id)).Take(15).ToList();
            var books = context.Books.Skip(5).Take(15).ToList();
            
            for (int i = 0; i < 15 && i < customers.Count && i < books.Count; i++)
            {
                context.Reservations.AddOrUpdate(res => res.ReservationId,
                    new Reservation
                    {
                        ReservationId = i + 1,
                        UserId = customers[i].Id,
                        BookId = books[i].BookId,
                        ReservationDate = DateTime.Now.AddDays(-10 + i),
                        ExpiryDate = DateTime.Now.AddDays(4 + i),
                        Status = i % 3 == 0 ? "Expired" : (i % 3 == 1 ? "Active" : "Fulfilled")
                    }
                );
            }
        }

        private void SeedDeliveries(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            for (int i = 0; i < 15; i++)
            {
                context.Deliveries.AddOrUpdate(d => d.DeliveryId,
                    new Delivery
                    {
                        DeliveryId = i + 1,
                        OrderId = (i % 15) + 1,
                        DeliveryDate = DateTime.Now.AddDays(-15 + i),
                        DeliveryAddress = $"{200 + i * 15} Delivery Avenue, City {i + 1}, State, {20000 + i}",
                        Status = i % 2 == 0 ? "Delivered" : "In Transit",
                        TrackingNumber = $"TRK{1000 + i}"
                    }
                );
            }
        }

        private void SeedPayments(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            for (int i = 0; i < 15; i++)
            {
                context.Payments.AddOrUpdate(p => p.PaymentId,
                    new Payment
                    {
                        PaymentId = i + 1,
                        OrderId = (i % 15) + 1,
                        PaymentDate = DateTime.Now.AddDays(-25 + i),
                        Amount = 25.99m + (i * 5),
                        PaymentMethod = i % 3 == 0 ? "Credit Card" : (i % 3 == 1 ? "PayPal" : "Bank Transfer"),
                        Status = i % 4 == 0 ? "Pending" : "Completed",
                        TransactionId = $"TXN{10000 + i}"
                    }
                );
            }
        }

        private void SeedReviews(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var customers = context.Users.Where(u => u.Roles.Any(r => r.RoleId == context.Roles.FirstOrDefault(role => role.Name == "Customer").Id)).Take(15).ToList();
            var books = context.Books.Take(15).ToList();
            
            string[] reviewTexts = {
                "Excellent book! Highly recommended.",
                "Great read, couldn't put it down.",
                "Good story but a bit slow in the middle.",
                "Amazing characters and plot development.",
                "Not my favorite but still worth reading.",
                "Fantastic book, one of the best I've read.",
                "Interesting concept but execution could be better.",
                "Loved every page of this book!",
                "Well written and engaging throughout.",
                "Good book for fans of the genre.",
                "Compelling story with great twists.",
                "Enjoyable read with memorable characters.",
                "Solid book, would recommend to others.",
                "Captivating from start to finish.",
                "Good addition to any book collection."
            };
            
            for (int i = 0; i < 15 && i < customers.Count && i < books.Count; i++)
            {
                context.Reviews.AddOrUpdate(r => r.ReviewId,
                    new Review
                    {
                        ReviewId = i + 1,
                        UserId = customers[i].Id,
                        BookId = books[i].BookId,
                        Rating = (i % 5) + 1,
                        ReviewText = reviewTexts[i],
                        ReviewDate = DateTime.Now.AddDays(-40 + i * 2)
                    }
                );
            }
        }

        private void SeedWishlists(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var customers = context.Users.Where(u => u.Roles.Any(r => r.RoleId == context.Roles.FirstOrDefault(role => role.Name == "Customer").Id)).Take(15).ToList();
            var books = context.Books.Skip(3).Take(15).ToList();
            
            for (int i = 0; i < 15 && i < customers.Count && i < books.Count; i++)
            {
                context.Wishlists.AddOrUpdate(w => w.WishlistId,
                    new Wishlist
                    {
                        WishlistId = i + 1,
                        UserId = customers[i].Id,
                        BookId = books[i].BookId,
                        DateAdded = DateTime.Now.AddDays(-50 + i * 3)
                    }
                );
            }
        }

        private void SeedNotifications(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var users = context.Users.Take(15).ToList();
            
            string[] messages = {
                "Your order has been shipped!",
                "New book available in your favorite category.",
                "Your rental is due tomorrow.",
                "Payment confirmation received.",
                "Your reservation is ready for pickup.",
                "Special discount available on selected books.",
                "Your review has been published.",
                "Book recommendation based on your reading history.",
                "Your order has been delivered.",
                "New arrivals in the Fiction category.",
                "Your wishlist item is now available.",
                "Account security update completed.",
                "Monthly newsletter with book recommendations.",
                "Your rental has been extended.",
                "Thank you for your recent purchase!"
            };
            
            for (int i = 0; i < 15 && i < users.Count; i++)
            {
                context.Notifications.AddOrUpdate(n => n.NotificationId,
                    new Notification
                    {
                        NotificationId = i + 1,
                        UserId = users[i].Id,
                        Message = messages[i],
                        IsRead = i % 3 == 0,
                        CreatedDate = DateTime.Now.AddDays(-30 + i * 2)
                    }
                );
            }
        }

        private void SeedShoppingCarts(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var customers = context.Users.Where(u => u.Roles.Any(r => r.RoleId == context.Roles.FirstOrDefault(role => role.Name == "Customer").Id)).Take(15).ToList();
            
            for (int i = 0; i < 15 && i < customers.Count; i++)
            {
                context.ShoppingCarts.AddOrUpdate(sc => sc.CartId,
                    new ShoppingCart
                    {
                        CartId = i + 1,
                        UserId = customers[i].Id,
                        CreatedDate = DateTime.Now.AddDays(-7 + i),
                        UpdatedDate = DateTime.Now.AddDays(-3 + i)
                    }
                );
            }
        }

        private void SeedCartItems(Connect2Us3._01.DAL.ApplicationDbContext context)
        {
            var books = context.Books.Take(15).ToList();
            
            for (int i = 0; i < 15 && i < books.Count; i++)
            {
                context.CartItems.AddOrUpdate(ci => ci.CartItemId,
                    new CartItem
                    {
                        CartItemId = i + 1,
                        CartId = (i % 15) + 1,
                        BookId = books[i].BookId,
                        Quantity = (i % 3) + 1,
                        DateAdded = DateTime.Now.AddDays(-5 + i)
                    }
                );
            }
        }
    }
}