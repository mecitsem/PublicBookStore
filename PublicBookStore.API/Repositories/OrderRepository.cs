using PublicBookStore.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicBookStore.API.Models;
using PublicBookStore.API.Data;

namespace PublicBookStore.API.Repositories
{
    public class OrderRepository : IOrderRepository, IDisposable
    {

        private PublicBookStoreEntities context;

        public OrderRepository()
        {
            this.context = new PublicBookStoreEntities();
        }

        public void AddOrUpdate(Order order)
        {
            if (context.Orders.Contains(order))
                context.Entry(order).State = System.Data.Entity.EntityState.Modified;
            else
                context.Orders.Add(order);
        }

        public void Delete(int id)
        {
            var order = context.Orders.Find(id);
            context.Orders.Remove(order);
        }

        public Order GetOrder(int id)
        {
            return context.Orders.Find(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return context.Orders.ToList();
        }

        public IEnumerable<Order> GetOrders(string username)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
