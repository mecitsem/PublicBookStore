using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.DTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Published { get; set; }
        public decimal Price { get; set; }
    }
}
