using System.Collections.Generic;

namespace PublicBookStore.API.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}