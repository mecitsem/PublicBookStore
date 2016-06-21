using System.Collections.Generic;

namespace PublicBookStore.API.Models
{
    public class Genre
    {

        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}
