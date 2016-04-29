using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public int GenreId { get; set; }

        public int AuthorId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime Published { get; set; }



        public virtual Genre Genre { get; set; }

        public virtual Author Author { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
