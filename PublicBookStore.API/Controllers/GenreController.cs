using AutoMapper;
using PublicBookStore.API.DTOs;
using PublicBookStore.API.Interfaces;
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
    public class GenreController : ApiController
    {
        private IGenreRepository _genreRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>());
        private MapperConfiguration configToEntity = new MapperConfiguration(cfg => cfg.CreateMap<GenreDTO, Genre>());

        public GenreController(IGenreRepository genreRepository)
        {
            this._genreRepo = genreRepository;
        }

        public IEnumerable<GenreDTO> Get()
        {
            var genres = _genreRepo.GetGenres();
            //Mapper
            var mapper = config.CreateMapper();

            return genres.AsEnumerable().Select(d => mapper.Map<Genre, GenreDTO>(d)).ToList();
        }

        public GenreDTO Get(int id)
        {
            var genre = _genreRepo.GetGenre(id);

            if (genre == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var mapper = config.CreateMapper();

            return mapper.Map<Genre, GenreDTO>(genre);
        }

        public HttpResponseMessage Post(GenreDTO genre)
        {
            HttpResponseMessage result = null;
            if (genre == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            try
            {
                var mapper = configToEntity.CreateMapper();
                var g = mapper.Map<GenreDTO, Genre>(genre);

                var updatedItem = _genreRepo.AddOrUpdate(g);
                _genreRepo.SaveChanges();
                result = Request.CreateResponse(HttpStatusCode.Created, config.CreateMapper().Map<Genre, GenreDTO>(updatedItem));
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
                _genreRepo.Delete(id);
                _genreRepo.SaveChanges();
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
            _genreRepo.Dispose();
            base.Dispose(disposing);
        }

    }
}
