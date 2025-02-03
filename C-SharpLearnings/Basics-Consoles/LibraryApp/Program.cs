using System;
using System.Linq;
using LibraryApp.Models;  // Ensure you have this for your models
using Microsoft.EntityFrameworkCore;  // Necessary for Include() method

namespace LibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LibraryContext())
            {
                //// 1. Create a new author and a book for the author
                var author = new Author { Name = "George Orwell" };
                context.Add(author); // Add the author to ensure Author is saved first
                Console.WriteLine("author added");
                try
                {
                    context.SaveChanges();  // Save changes to the database
                    Console.WriteLine("Changes saved successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving changes: {ex.Message}");
                }

                var book = new Book { Title = "1984", Author = author };
                context.Add(book);  // Add the book to the database
                context.SaveChanges();  // Save book to database
                Console.WriteLine("Book added!");

                // 2. Read and display all books in the library
                var books = context.Books.Include(b => b.Author).ToList();  // Include author for each book
                Console.WriteLine("Books in Library:");
                foreach (var b in books)
                {
                    Console.WriteLine($"{b.Title} by {b.Author.Name}");
                }

                // 3. Update the first book's title (ensure there is at least one book)
                var firstBook = context.Books.FirstOrDefault();
                if (firstBook != null)
                {
                    firstBook.Title = "Animal Farm";
                    context.SaveChanges();
                    Console.WriteLine($"Updated book title to: {firstBook.Title}");
                }

                // 4. Delete the first book (ensure there is at least one book)
                if (firstBook != null)
                {
                    context.Books.Remove(firstBook);
                    context.SaveChanges();
                    Console.WriteLine("Book deleted.");
                }
                else
                {
                    Console.WriteLine("No books found to delete.");
                }
            }
        }
    }
}
