using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;

namespace PublicBookStore.API.Interfaces
{
    public interface IStoreRepository : IDisposable
    {
        IEnumerable<Cart> GetCarts();
        IEnumerable<Cart> GetCarts(string cartId);
        IEnumerable<Cart> GetCartsByBookId(int bookId);
        void SaveChanges();
        Cart AddOrUpdate(Cart cart);
        void Delete(int id);

    }
}
