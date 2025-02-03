using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Author
    {
        public int AuthorId { get; set; } // Primary key
        public string Name { get; set; }

        // Navigation property for one-to-many relationship (one author can write many books)
        public ICollection<Book> Books { get; set; }
    }
}
