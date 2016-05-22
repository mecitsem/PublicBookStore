using AutoMapper;
using PublicBookStore.API.DTOs;
using PublicBookStore.API.Interfaces;
using PublicBookStore.API.Models;
using PublicBookStore.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PublicBookStore.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrderController : ApiController
    {
        private IOrderRepository _orderRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>());
        private MapperConfiguration configToEntity = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, Order>());

        public OrderController(IOrderRepository orderRepository)
        {
            this._orderRepo = orderRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">username</param>
        /// <returns></returns>
        public IEnumerable<OrderDTO> Get(string id)
        {
            var orders = _orderRepo.GetOrders(id);
            var mapper = config.CreateMapper();
            return orders.AsEnumerable().Select(a => mapper.Map<Order, OrderDTO>(a));
        }

        protected override void Dispose(bool disposing)
        {
            _orderRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
