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
    public class BookController : ApiController
    {
        private BookRepository _bookRepo;

        public BookController()
        {
            _bookRepo = new BookRepository();
        }

        public IEnumerable<Book> Get()
        {
            return _bookRepo.GetBooks();
        }

        public Book Get(int id)
        {
            return _bookRepo.GetBook(id);
        }

        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }
    }
}
