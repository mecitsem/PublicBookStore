using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.DTOs
{
    public class CartDTO
    {
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
