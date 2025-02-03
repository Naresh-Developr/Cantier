using LibraryApp.Models;

public class Book
{
    public int BookId { get; set; }  // Primary key
    public string Title { get; set; }  // Title of the book
    public int AuthorId { get; set; }  // Foreign key for the author
    public Author Author { get; set; }  // Navigation property for the author
}
