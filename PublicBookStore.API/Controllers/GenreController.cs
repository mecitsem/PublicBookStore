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
            var genre = _genreRepo.GetGenre(id);
            if (genre == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return genre;
        }

        public void Post(Genre genre)
        {
            if (genre == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);

            _genreRepo.AddOrUpdate(genre);
            _genreRepo.SaveChanges();
        }

        public void Put(int id)
        {
            var genre = _genreRepo.GetGenre(id);
            if (genre == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);
            _genreRepo.AddOrUpdate(genre);
            _genreRepo.SaveChanges();
        }

        public void Delete(int id)
        {
            _genreRepo.Delete(id);
            _genreRepo.SaveChanges();
        }

    }
}
