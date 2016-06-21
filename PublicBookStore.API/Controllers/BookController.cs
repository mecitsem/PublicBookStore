using AutoMapper;
using PublicBookStore.API.DTOs;
using PublicBookStore.API.Interfaces;
using PublicBookStore.API.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PublicBookStore.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookController : ApiController
    {
        #region Fields
        private IBookRepository _bookRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>());
        private MapperConfiguration configToEntity = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>());
        #endregion

        #region Constructors
        public BookController(IBookRepository bookRepository)
        {
            _bookRepo = bookRepository;
        }
        #endregion

        #region Methods
        public HttpResponseMessage Get()
        {
            var books = _bookRepo.GetBooks();
            //Mapper
            var mapper = config.CreateMapper();
            var content = books.AsEnumerable().Select(dto => mapper.Map<Book, BookDTO>(dto)).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, content);
        }

        public HttpResponseMessage Get(int id)
        {
            var book = _bookRepo.GetBook(id);

            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Mapper
            var mapper = config.CreateMapper();
            var content = mapper.Map<Book, BookDTO>(book);
            return Request.CreateResponse(HttpStatusCode.OK, content);
        }

        public HttpResponseMessage Post(BookDTO book)
        {
            HttpResponseMessage result;
            try
            {
                if (book == null)
                    throw new HttpResponseException(HttpStatusCode.NoContent);

                var mapper = configToEntity.CreateMapper();
                var b = mapper.Map<BookDTO, Book>(book);

                var updatedItem = _bookRepo.AddOrUpdate(b);
                _bookRepo.SaveChanges();
                result = Request.CreateResponse(HttpStatusCode.Created, config.CreateMapper().Map<Book, BookDTO>(updatedItem));
            }
            catch (Exception ex)
            {
                result = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
            }
            return result;
        }



        // DELETE api/book/5
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage result;
            try
            {
                _bookRepo.Delete(id);
                _bookRepo.SaveChanges();
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
            _bookRepo.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
