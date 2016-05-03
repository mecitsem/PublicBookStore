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
            return _authorRepo.GetAuthor(id);
        }

        
    }
}
