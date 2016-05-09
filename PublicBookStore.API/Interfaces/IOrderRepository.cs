using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.Interfaces
{
    public interface IOrderRepository : IDisposable
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrders(string username);
        Order GetOrder(int id);
        void AddOrUpdate(Order order);
        void Delete(int id);
        void SaveChanges();
    }
}
