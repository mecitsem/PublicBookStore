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
    public class GenreController : ApiController
    {
        private GenreRepository _genreRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>());
        private MapperConfiguration configToEntity = new MapperConfiguration(cfg => cfg.CreateMap<GenreDTO, Genre>());
        public GenreController()
        {
            _genreRepo = new GenreRepository();
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
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapper = config.CreateMapper();

            return mapper.Map<Genre, GenreDTO>(genre);
        }

        public void Post(GenreDTO genre)
        {
            if (genre == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);

            var mapper = configToEntity.CreateMapper();
            var g = mapper.Map<GenreDTO, Genre>(genre);

            _genreRepo.AddOrUpdate(g);
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
