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
        IEnumerable<Cart> GetCarts(string cartId);
        IEnumerable<Cart> GetCartsByBookId(int bookId);
        void SaveChanges();
    }
}
