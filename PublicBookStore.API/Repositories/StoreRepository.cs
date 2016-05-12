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

        public IEnumerable<Cart> GetCartsByBookId(int bookId)
        {
            return context.Carts.Where(c => c.BookId.Equals(bookId)).ToList();
        }

        public Cart AddOrUpdate(Cart cart)
        {
            Cart result = null;
            if (context.Carts.Any(c => c.RecordId.Equals(cart.RecordId)))
            {
                var exCart = context.Carts.Find(cart.RecordId);
                exCart.CartId = cart.CartId;
                exCart.Count = cart.Count;
                exCart.DateCreated = cart.DateCreated;
                exCart.BookId = cart.BookId;
                context.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                result = exCart;
            }
            else
              result=  context.Carts.Add(cart);

            return result;
        }

        public void Delete(int id)
        {
            var cart = context.Carts.Find(id);
            if (cart == null)
                throw new ArgumentNullException();

            context.Carts.Remove(cart);

        }

        public void AddOrUpdateOrderDetail(OrderDetail orderDetail)
        {
            if (context.OrderDetails.Contains(orderDetail))
                context.Entry(orderDetail).State = System.Data.Entity.EntityState.Modified;
            else
                context.OrderDetails.Add(orderDetail);
        }

        public IEnumerable<Cart> GetCarts(string cartId)
        {
            return context.Carts.Where(c => c.CartId.Equals(cartId));
        }
    }
}
