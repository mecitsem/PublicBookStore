﻿using AutoMapper;
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
    public class StoreController : ApiController
    {

        #region Fields
        private IStoreRepository _storeRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Cart, CartDTO>());
        private MapperConfiguration configToEntity = new MapperConfiguration(cfg => cfg.CreateMap<CartDTO, Cart>());
        #endregion

        #region Constructors
        public StoreController(IStoreRepository storeRepository)
        {
            this._storeRepo = storeRepository;
        }
        #endregion

        #region Methods
        // GET api/store/5
        public HttpResponseMessage Get(string id)
        {
            var carts = _storeRepo.GetCarts(id);

            if (carts == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Mapper
            var mapper = config.CreateMapper();
            var content = carts.AsEnumerable().Select(c => mapper.Map<Cart, CartDTO>(c)).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, content);
        }

        public HttpResponseMessage Post(CartDTO cart)
        {
            HttpResponseMessage result = null;
            try
            {
                if (cart == null)
                    throw new HttpResponseException(HttpStatusCode.NoContent);

                var mapper = configToEntity.CreateMapper();

                var c = mapper.Map<CartDTO, Cart>(cart);

                var updatedItem = _storeRepo.AddOrUpdate(c);

                _storeRepo.SaveChanges();

                result = Request.CreateResponse(HttpStatusCode.Created, config.CreateMapper().Map<Cart, CartDTO>(updatedItem));

            }
            catch (Exception ex)
            {
                result = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
            }
            return result;
        }


        // DELETE api/store/5
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage result = null;
            try
            {
                _storeRepo.Delete(id);
                _storeRepo.SaveChanges();
                result = Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                result = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
            }
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            _storeRepo.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
