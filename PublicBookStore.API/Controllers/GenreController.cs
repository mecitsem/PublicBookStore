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
    public class GenreController : ApiController
    {
        private GenreRepository _genreRepo;

        public GenreController()
        {
            _genreRepo = new GenreRepository();
        }

        public IEnumerable<Genre> Get()
        {
            return _genreRepo.GetGenres();
        }

        public Genre Get(int id)
        {
            return _genreRepo.GetGenre(id);
        }
    }
}
