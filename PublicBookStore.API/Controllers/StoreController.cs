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
        private BookRepository _bookRepo;

        public StoreController()
        {
            _storeRepo = new StoreRepository();
            _bookRepo = new BookRepository();
        }

        // GET api/store
        public IEnumerable<Book> Get()
        {
            return _bookRepo.GetBooks();
        }

        // GET api/store/5
        public Book Get(int id)
        {
            return _bookRepo.GetBook(id);
        }

        // POST api/store
        public void Post([FromBody]string value)
        {
        }

        // PUT api/store/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/store/5
        public void Delete(int id)
        {
        }
    }
}
