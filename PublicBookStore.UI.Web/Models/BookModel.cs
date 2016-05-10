using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PublicBookStore.UI.Web.Models
{
    [Bind(Exclude = "BookId")]
    public class BookModel
    {
        [ScaffoldColumn(false)]
        public int BookId { get; set; }

        [DisplayName("Genre")]
        public int GenreId { get; set; }

        [DisplayName("Author")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "An Author Title is required")]
        [StringLength(160)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100.00,
            ErrorMessage = "Price must be between 0.01 and 100.00")]
        public decimal Price { get; set; }

        [DisplayName("Book Art URL")]
        [StringLength(1024)]
        public string AlbumArtUrl { get; set; }

    }
}
