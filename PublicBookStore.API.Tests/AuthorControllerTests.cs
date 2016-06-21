using PublicBookStore.API.Controllers;
using PublicBookStore.API.DTOs;
using System.Collections.Generic;
using System.Linq;
using PublicBookStore.API.Interfaces;
using System.Net.Http;
using PublicBookStore.API.Models;
using AutoMapper;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;

namespace PublicBookStore.API.Tests
{
    [TestFixture]
    public class AuthorControllerTests
    {
        private IUnityContainer _container;
        private Mock<IAuthorRepository> mock;
        [SetUp]
        public void Setup()
        {
            _container = new UnityContainer();
            mock = new Mock<IAuthorRepository>();
            _container.RegisterInstance(mock.Object);
        }

        /// <summary>
        /// GET Authors /api/author
        /// </summary>
        [Test]
        public void GetAuthors_ShouldReturnAllAuthors()
        {
            //Arrange
            var authors = SetupAuthors();

            if (authors == null) Assert.Fail("Authors collection is null");

            //Create Mock
            mock.Setup(x => x.GetAuthors()).Returns(authors);

            var authorController = new AuthorController(mock.Object);
            UnitTestHelper.SetupControllerForTests(authorController);

            //Act
            var result = authorController.Get();
            IEnumerable<AuthorDTO> resultAurhors;
            result.TryGetContentValue(out resultAurhors);

            //Assert
            Assert.IsNotNull(resultAurhors);

        }

        /// <summary>
        /// Get Author /api/author/1
        /// </summary>
        [Test]
        public void GetAuthor_ShouldReturnFirstAuthor()
        {
            //Author
            var author = SetupAuthors().FirstOrDefault(s => s.AuthorId.Equals(1));
            if (author == null) Assert.Fail("Author is null");

            //Create Mock
            mock.Setup(x => x.GetAuthor(1)).Returns(author);

            //Act
            var authorController = new AuthorController(mock.Object);
            UnitTestHelper.SetupControllerForTests(authorController);
            var result = authorController.Get(1); //J. K. Rowling
            AuthorDTO responseAuthorDto;
            result.TryGetContentValue(out responseAuthorDto);

            //Assert
            Assert.IsNotNull(responseAuthorDto);
            Assert.AreSame(author.Name, responseAuthorDto.Name);
        }


        #region Private member methods

        private static IEnumerable<Author> SetupAuthors()
        {
            var authors = new List<Author>
            {
                new Author {AuthorId = 1, Name = "J. K. Rowling"},
                new Author {AuthorId = 2, Name = "Bella Forrest"},
                new Author {AuthorId = 3, Name = "David Baldacci"}
            };
            return authors;
        }

        //private Mock<TRepo> SetupRepository<TModel, TRepo>(IEnumerable<TModel> models)
        //    where TModel : Author, new()
        //    where TRepo : class, IAuthorRepository
        //{
        //    var mock = new Mock<TRepo>();
        //    container.RegisterInstance<IAuthorRepository>(mock.Object);
        //    mock.Setup(x => x.GetAuthors()).Returns(models);
        //    return mock;
        //}



        #endregion

    }
}
