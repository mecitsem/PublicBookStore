using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;

namespace PublicBookStore.API.Interfaces
{
    public interface IOrderRepository : IDisposable
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrders(string username);
        Order GetOrder(int id);
        Order AddOrUpdate(Order order);
        void Delete(int id);
        void SaveChanges();
    }
}
