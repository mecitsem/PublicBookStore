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
    public class GenreController : ApiController
    {
        #region Fields
        private IGenreRepository _genreRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>());
        private MapperConfiguration configToEntity = new MapperConfiguration(cfg => cfg.CreateMap<GenreDTO, Genre>());
        #endregion

        #region Constructors
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepo = genreRepository;
        }
        #endregion

        #region Methods
        public HttpResponseMessage Get()
        {
            var genres = _genreRepo.GetGenres();
            //Mapper
            var mapper = config.CreateMapper();
            var content = genres.AsEnumerable().Select(d => mapper.Map<Genre, GenreDTO>(d)).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, content);
        }

        public HttpResponseMessage Get(int id)
        {
            var genre = _genreRepo.GetGenre(id);

            if (genre == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var mapper = config.CreateMapper();
            var content = mapper.Map<Genre, GenreDTO>(genre);
            return Request.CreateResponse(HttpStatusCode.OK, content);
        }

        public HttpResponseMessage Post(GenreDTO genre)
        {
            HttpResponseMessage result;
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
            HttpResponseMessage result;
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
        #endregion
    }
}
