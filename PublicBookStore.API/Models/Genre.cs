using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
