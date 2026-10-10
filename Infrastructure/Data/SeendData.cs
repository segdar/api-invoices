using ApiInvoices.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiInvoices.Infrastructure.Data;

 public class SeendData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // password: admin123 (bcrypt hash)
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Name = "Administrator",
                Email = "admin@invoices.com",
                PasswordHash = "$2b$11$D5LrW1xtwO0JzvcGpjYQi.X.JYKNyMye361wTCa2M9D.C..dIwiGq",
                Status = "active"
            });

            modelBuilder.Entity<Client>().HasData(
                new Client { ClientId = 1, Name = "Juan", LastName = "Perez", Address = "Ciudad de Guatemala" },
                new Client { ClientId = 2, Name = "Maria", LastName = "Lopez", Address = "Antigua Guatemala" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Description = "Laptop" },
                new Product { ProductId = 2, Description = "Mouse" },
                new Product { ProductId = 3, Description = "Keyboard" }
            );
        }
    }

