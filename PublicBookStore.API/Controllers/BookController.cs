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
    public class BookController : ApiController
    {
        private BookRepository _bookRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>());
        public BookController()
        {
            _bookRepo = new BookRepository();
        }

        public IEnumerable<BookDTO> Get()
        {
            var books = _bookRepo.GetBooks();
            //Mapper
            var mapper = config.CreateMapper();

            return books.AsEnumerable().Select(dto => mapper.Map<Book, BookDTO>(dto));
        }

        public BookDTO Get(int id)
        {
            var book = _bookRepo.GetBook(id);

            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Mapper
            var mapper = config.CreateMapper();

            return mapper.Map<Book, BookDTO>(book);
        }

        public void Post(BookDTO book)
        {
            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);

            var 

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
