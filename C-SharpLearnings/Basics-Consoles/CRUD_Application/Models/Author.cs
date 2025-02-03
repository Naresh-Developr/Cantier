using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Application.Models {

    public class Author
    {
        public int AuthorId { get; set; } // Primary key
        public string Name { get; set; } = string.Empty;

        // Navigation property for one-to-many relationship (one author can write many books)
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }



}
