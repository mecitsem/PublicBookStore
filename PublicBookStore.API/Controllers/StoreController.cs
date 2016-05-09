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

        public StoreController()
        {
            _storeRepo = new StoreRepository();
        }


        // GET api/store/5
        public Cart Get(int id)
        {
            var cart =  _storeRepo.GetCart(id);

            if (cart == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return cart;
        }

        [HttpGet, ActionName("GetCarts")]
        public IEnumerable<Cart> GetCarts(int bookId)
        {
            return _storeRepo.GetCartsByBookId(bookId);
        }



        // DELETE api/store/5
        public void Delete(int id)
        {
            //_storeRepo.Delete(id);
        }
    }
}
