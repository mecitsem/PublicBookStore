using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.Models
{
    public class Cart
    {

        public int CartId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Book Book { get; set; }
    }
}
