using System;
using CRUD_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to project 1:[Crud With Entity Framework]");
            //Console.WriteLine("Option available: \n 1.AddBook and Author \n 2.List All books \n 3.Update \n 4.Delete");
            

            var operations = new Operations();  // Create instance of Operations class

            while (true)
            {
                Console.WriteLine("----------------------------------------- :(");
                Console.WriteLine("Option available: \n 1.AddBook and Author \n 2.List All books \n 3.Update \n 4.Delete \n 5.Find By Title \n 6.Exit");
                Console.WriteLine("----------------------------------------- :)");
                string choice = Console.ReadLine() ?? string.Empty;

                if (choice == "1")
                {
                    Console.WriteLine("Selected: Create");
                    Console.WriteLine("Enter Author Name");
                    string author = Console.ReadLine();
                    Console.WriteLine("Enter Book Name");
                    string bookName = Console.ReadLine();

                    operations.Myadd(author, bookName);  // Use the instance to call Myadd

                    Console.WriteLine("Book added successfully");
                }
                if (choice == "2")
                {
                    Console.WriteLine("Selected: Read");
                    operations.MyRead();

                }
                if (choice == "3")
                {
                    Console.WriteLine("Selected: Update");
                    operations.MyRead();
                    Console.WriteLine("Enter: Book id");
                    int bi = Convert.ToInt32(Console.ReadLine());
                    operations.MyUpdate(bi);

                }
                if (choice == "4")
                {
                    Console.WriteLine("Selected: Delete");
                    operations.MyRead();
                    Console.WriteLine("Enter: Book id");
                    int bi = Convert.ToInt32(Console.ReadLine());
                    operations.MyDelete(bi);

                }
                if (choice == "5")
                {
                    Console.WriteLine("Selected: Find");
                    Console.WriteLine("Enter the book Name to find:");
                    string name = Console.ReadLine();
                    operations.MyFind(name);
                }
                if (choice == "6")
                {
                    break;
                }
            }
        }
    }

    public class Operations
    {
        private int Tbooks = 0;
        public readonly LibraryContext _context;

        public Operations()
        {
            _context = new LibraryContext();
        }

        public void Myadd(string auth, string book)
        {
            var author = new Author { Name = auth };
            var bookObj = new Book { Title = book, Author=author };

            // Add the author and book to the context
            _context.Add(author);
            _context.Add(bookObj);
            _context.SaveChanges();
        }

        public void MyRead()
        {
            var books = _context.Books.Include(b => b.Author).ToList();  // Include author for each book
            Console.WriteLine("Books in Library:");
            
            foreach (var b in books)
            {
                if (b.Author != null)
                {
                    Console.WriteLine($"Id: {b.BookId}| Title: {b.Title} | Author: {b.Author.Name}");
                }
                else
                {
                    Console.WriteLine($"Id: {b.BookId} | Title: {b.Title} | Author: [No Author Assigned]");
                }
                Tbooks++;

            }
        }

        public void MyUpdate(int bookId)
        {
            var books = _context.Books.Include(b => b.Author).FirstOrDefault(b => b.BookId == bookId);

            if (books != null)
            {
                if (books.Author.Name != null)
                {
                    Console.WriteLine("Selected Author name: " + books.Author.Name + "| selected Book Name: " + books.Title);

                    Console.WriteLine("Enter New Author Name to change: ");

                    string NewAuthor = Console.ReadLine();

                    books.Author.Name = NewAuthor;

                    _context.SaveChanges();

                    Console.WriteLine("Book updated successfully.");

                }
                else
                {
                    Console.WriteLine("Author not found");
                }
            }
        }

        public void MyDelete(int BookId)
        {
            var books = _context.Books.Include(b =>b.Author).FirstOrDefault(b => b.BookId == BookId);

            if(books != null)
            {
                _context.Books.Remove(books);
                _context.SaveChanges();
                Console.WriteLine("Book removed Sucessfully");
            }
            else
            {
                Console.WriteLine("There is no data to delete");
            }
        }

        public void MyFind(string Bname)
        {
            var books = _context.Books.Include(b => b.Author).ToList();
            int flag = 0;
            foreach (var book in books)
            {
                if (book.Title == Bname)
                {
                    Console.WriteLine("Yes the book is Here ::");
                    flag++;

                }
            }
            if (flag <= 0)
            {
                Console.WriteLine("no");
            }




        }
    }
}
