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
    public class AuthorController : ApiController
    {
        private AuthorRepository _authorRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>());

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

        public void Post(AuthorDTO author)
        {
            if (author == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);

            var mapper = config.CreateMapper();
            var a = mapper.Map<AuthorDTO, Author>(author);

            _authorRepo.AddOrUpdate(a);
            _authorRepo.SaveChanges();
        }

        public void Put(int id)
        {
            var author = _authorRepo.GetAuthor(id);
            if (author == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);

            _authorRepo.AddOrUpdate(author);
            _authorRepo.SaveChanges();
        }


        public void Delete(int id)
        {
            _authorRepo.Delete(id);
            _authorRepo.SaveChanges();
        }

    }
}
