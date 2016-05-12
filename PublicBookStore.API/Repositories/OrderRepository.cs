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

        public Order AddOrUpdate(Order order)
        {
            Order result = null;
            if (context.Orders.Any(o => o.OrderId.Equals(order.OrderId)))
            {
                var exOrder = context.Orders.Find(order.OrderId);
                exOrder.Address = order.Address;
                exOrder.City = order.City;
                exOrder.Country = order.Country;
                exOrder.Email = order.Email;
                exOrder.FirstName = order.FirstName;
                exOrder.LastName = order.LastName;
                exOrder.OrderDate = order.OrderDate;
                exOrder.Phone = order.Phone;
                exOrder.PostalCode = order.PostalCode;
                exOrder.State = order.State;
                exOrder.Total = order.Total;
                exOrder.Username = order.Username;
                context.Entry(order).State = System.Data.Entity.EntityState.Modified;
                result = exOrder;
            }
            else
                result = context.Orders.Add(order);

            return result;
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
