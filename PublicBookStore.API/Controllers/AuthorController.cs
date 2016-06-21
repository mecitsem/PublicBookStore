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
    public class AuthorController : ApiController
    {
        #region Fields
        private IAuthorRepository _authorRepo;
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorDTO>());
        private MapperConfiguration configToEntity = new MapperConfiguration(cfg => cfg.CreateMap<AuthorDTO, Author>());
        #endregion

        #region Constructors
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepo = authorRepository;
        }
        #endregion

        #region Methods
        public HttpResponseMessage Get()
        {

            var authors = _authorRepo.GetAuthors();
            var mapper = config.CreateMapper();
            var content = authors.AsEnumerable().Select(a => mapper.Map<Author, AuthorDTO>(a)).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, content);
        }

        public HttpResponseMessage Get(int id)
        {


            var author = _authorRepo.GetAuthor(id);

            if (author == null)
                throw new HttpResponseException(HttpStatusCode.NoContent);
            var mapper = config.CreateMapper();
            var content = mapper.Map<Author, AuthorDTO>(author);
            return Request.CreateResponse(HttpStatusCode.OK, content);
        }

        public HttpResponseMessage Post(AuthorDTO author)
        {

            HttpResponseMessage result;
            try
            {
                if (author == null)
                    throw new HttpResponseException(HttpStatusCode.NoContent);

                var mapper = configToEntity.CreateMapper();
                var a = mapper.Map<AuthorDTO, Author>(author);

                var updatedItem = _authorRepo.AddOrUpdate(a);
                _authorRepo.SaveChanges();
                var content = config.CreateMapper().Map<Author, AuthorDTO>(updatedItem);
                result = Request.CreateResponse(HttpStatusCode.Created, content);

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
                _authorRepo.Delete(id);
                _authorRepo.SaveChanges();
                result = Request.CreateResponse(HttpStatusCode.OK);
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
        #endregion

    }
}
