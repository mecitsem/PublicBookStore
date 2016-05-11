using AutoMapper;
using PublicBookStore.API.DTOs;
using PublicBookStore.API.Models;
using PublicBookStore.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PublicBookStore.API.Controllers
{
    public class StoreController : ApiController
    {
        private StoreRepository _storeRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Cart, CartDTO>());
        private MapperConfiguration configToEntity = new MapperConfiguration(cfg => cfg.CreateMap<CartDTO, Cart>());

        public StoreController()
        {
            _storeRepo = new StoreRepository();
        }


        // GET api/store/5
        public IEnumerable<CartDTO> Get(string id)
        {
            var carts = _storeRepo.GetCarts(id);

            if (carts == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Mapper
            var mapper = config.CreateMapper();

            return carts.AsEnumerable().Select(c => mapper.Map<Cart, CartDTO>(c));
        }

        public void Post(CartDTO cart)
        {
            if (cart == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);

            var mapper = configToEntity.CreateMapper();

            var c = mapper.Map<CartDTO, Cart>(cart);

            _storeRepo.AddOrUpdate(c);
            _storeRepo.SaveChanges();
        }


        // DELETE api/store/5
        public void Delete(int id)
        {
            _storeRepo.Delete(id);
            _storeRepo.SaveChanges();
        }
    }
}
