using System;
using Connect2Us3._01.DAL;
using Connect2Us3._01.Migrations;

namespace Seeder
{
    public class SeedDatabase
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting database seeding...");

                var configuration = new Configuration();

                Console.WriteLine("Seeding users and roles...");
                using (var context = new ApplicationDbContext())
                {
                    configuration.SeedUsersAndRoles(context);
                    context.SaveChanges();
                }

                Console.WriteLine("Seeding initial data...");
                using (var context = new ApplicationDbContext())
                {
                    configuration.SeedInitialData(context);
                    context.SaveChanges();
                }

                Console.WriteLine("Database seeding completed successfully!");
                Console.WriteLine("\nCreated Users:");
                Console.WriteLine("Admin User: admin@connect2us.com / Admin123!");
                Console.WriteLine("Staff User: staff@connect2us.com / Staff123!");
                Console.WriteLine("Customer Users:");
                for (int i = 1; i <= 13; i++)
                {
                    Console.WriteLine($"  customer{i}@connect2us.com / Customer123!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during seeding: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}