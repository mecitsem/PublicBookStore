using PublicBookStore.API.Data;
using PublicBookStore.API.Interfaces;
using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.Repositories
{
    public class StoreRepository : IStoreRepository, IDisposable
    {
        private PublicBookStoreEntities context;

        public StoreRepository()
        {
            this.context = new PublicBookStoreEntities();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IEnumerable<Cart> GetCarts()
        {
            return context.Carts.ToList();
        }

        public Cart GetCart(int id)
        {
            return context.Carts.Find(id);
        }

        public IEnumerable<Cart> GetCartsByBookId(int bookId)
        {
            return context.Carts.Where(c => c.BookId.Equals(bookId)).ToList();
        }
    }
}
