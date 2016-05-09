using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.Interfaces
{
    public interface IStoreRepository : IDisposable
    {
        IEnumerable<Cart> GetCarts();
        Cart GetCart(int id);
        IEnumerable<Cart> GetCartsByBookId(int bookId);
        void SaveChanges();
    }
}
