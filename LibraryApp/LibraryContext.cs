using LibraryApp.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{

  

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    // Configure the connection to the database (use your own connection string here)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=LibraryDb;Trusted_Connection=True;trustServerCertificate=true;");

        Console.WriteLine("connection buid");
    }
}
