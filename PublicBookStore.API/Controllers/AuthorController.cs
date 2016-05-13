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
using System.Web.Http.Cors;

namespace PublicBookStore.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthorController : ApiController
    {
        private AuthorRepository _authorRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>());
        private MapperConfiguration configToEntity = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>());

        public AuthorController()
        {
            _authorRepo = new AuthorRepository();
        }

        public IEnumerable<AuthorDTO> Get()
        {
            var authors = _authorRepo.GetAuthors();
            var mapper = config.CreateMapper();
            return authors.AsEnumerable().Select(a => mapper.Map<Author, AuthorDTO>(a));
        }

        public AuthorDTO Get(int id)
        {
            var author = _authorRepo.GetAuthor(id);

            if (author == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);
            var mapper = config.CreateMapper();

            return mapper.Map<Author, AuthorDTO>(author);
        }

        public HttpResponseMessage Post(AuthorDTO author)
        {
            HttpResponseMessage result = null;
            try
            {
                if (author == null)
                    throw new HttpResponseException(HttpStatusCode.NoContent);

                var mapper = configToEntity.CreateMapper();
                var a = mapper.Map<AuthorDTO, Author>(author);

                var updatedItem = _authorRepo.AddOrUpdate(a);
                _authorRepo.SaveChanges();
                result = Request.CreateResponse(HttpStatusCode.Created, config.CreateMapper().Map<Author, AuthorDTO>(updatedItem));
            }
            catch (Exception ex)
            {
                result = Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
            }

            return result;
        }


        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage result = null;
            try
            {
                _authorRepo.Delete(id);
                _authorRepo.SaveChanges();
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
            _authorRepo.Dispose();
            base.Dispose(disposing);
        }

    }
}
