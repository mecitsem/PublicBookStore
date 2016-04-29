using System.ComponentModel.DataAnnotations;

namespace PublicBookStore.API.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}