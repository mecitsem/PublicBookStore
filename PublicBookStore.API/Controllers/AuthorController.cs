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

        public AuthorController()
        {
            _authorRepo = new AuthorRepository();
        }

        public IEnumerable<Author> Get()
        {
            return _authorRepo.GetAuthors();
        }

        public Author Get(int id)
        {
            var author = _authorRepo.GetAuthor(id);

            if (author == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);

            return author;
        }

        public void Post(Author author)
        {
            _authorRepo.AddOrUpdate(author);
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
