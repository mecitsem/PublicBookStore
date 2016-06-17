using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicBookStore.API.Controllers;
using PublicBookStore.API.DTOs;
using System.Collections.Generic;
using PublicBookStore.API.Interfaces;
using PublicBookStore.API.Repositories;
using System.Net.Http;
using System.Web.Http;
using PublicBookStore.API.Models;
using PublicBookStore.API.Data;
using System.Linq;
using Microsoft.Practices.Unity;
using AutoMapper;
using Moq;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq.Expressions;

namespace PublicBookStore.API.Tests
{

    public class AuthorControllerTests
    {
        private MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorDTO>());
        private MapperConfiguration configToEntity = new MapperConfiguration(cfg => cfg.CreateMap<AuthorDTO, Author>());
        private IMapper mapperToEntity;
        private IMapper mapperToDTO;
        [TestInitialize]
        public void Setup()
        {
            mapperToDTO = config.CreateMapper();
            mapperToEntity = configToEntity.CreateMapper();
        }


        [TestMethod]
        public void GetAuthors_ShouldReturnAllAuthors()
        {

            var authors = SetupAuthors();
            var authorRepo = SetupRepository<Author, IAuthorRepository>(authors);

            AuthorController authorController = new AuthorController(authorRepo.Object);
            var result = authorController.Get();
            List<AuthorDTO> resultAurhors;
            result.TryGetContentValue(out resultAurhors);
            CollectionAssert.AreEqual(authors, resultAurhors.AsEnumerable().Select(c => mapperToEntity.Map<AuthorDTO, Author>(c)).ToList());

        }


        #region Private member methods

        private List<Author> SetupAuthors()
        {
            var authors = new List<Author>();
            authors.Add(new Author { AuthorId = 1, Name = "J. K. Rowling" });
            authors.Add(new Author { AuthorId = 2, Name = "Bella Forrest" });
            authors.Add(new Author { AuthorId = 3, Name = "David Baldacci" });
            return authors;
        }

        private Mock<TRepo> SetupRepository<TModel, TRepo>(List<TModel> models)
            where TModel : Author, new()
            where TRepo : class, IAuthorRepository
        {
            Mock<TRepo> repository = new Mock<TRepo>();
            repository.Setup(x => x.GetAuthors()).Returns(models);

            return repository;
        }

        #endregion

    }
}
