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
            var book = _bookRepo.GetBook(id);

            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return book;
        }

        public void Post(Book book)
        {
            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);

            _bookRepo.AddOrUpdate(book);
            _bookRepo.SaveChanges();
        }

        // PUT api/book/5
        public void Put(int id)
        {
            //Find book
            var book = _bookRepo.GetBook(id);

            //Check book
            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Update existing book
            _bookRepo.AddOrUpdate(book);

            //Save
            _bookRepo.SaveChanges();
        }

        // DELETE api/book/5
        public void Delete(int id)
        {
            _bookRepo.Delete(id);
            _bookRepo.SaveChanges();
        }
    }
}
