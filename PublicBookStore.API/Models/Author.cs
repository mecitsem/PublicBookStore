using System.ComponentModel.DataAnnotations;

namespace PublicBookStore.API.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string Name { get; set; }
    }
}