using CRUD_Application.Models;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;  // Initialize
    public int AuthorId { get; set; }
    public Author Author { get; set; } = new Author();  // Initialize the navigation property
}

