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

        IUnityContainer _container = new UnityContainer();
      

        [SetUp]
        public void Setup()
        {
           
        }


        [Test]
        public void GetAuthors_ShouldReturnAllAuthors()
        {

            var authors = SetupAuthors();

            if(authors == null) Assert.Fail("Authors collection is null");

            //Create Mock
            var mock = new Mock<IAuthorRepository>();
            _container.RegisterInstance(mock.Object);
            mock.Setup(x => x.GetAuthors()).Returns(authors);


            //Act
            var authorController = new AuthorController(mock.Object);
            UnitTestHelper.SetupControllerForTests(authorController);
            var result = authorController.Get();
            IEnumerable<AuthorDTO> resultAurhors;
            result.TryGetContentValue(out resultAurhors);
            
            //Result
            Assert.IsNotNull(resultAurhors);

        }

        [Test]
        public void GetAuthor_ShouldReturnFirstAuthor()
        {
            //Author
            var author = SetupAuthors().FirstOrDefault(s => s.AuthorId == 1);
            
            if(author == null) Assert.Fail("Author is null");

            //Create Mock
            var mock = new Mock<IAuthorRepository>();
            _container.RegisterInstance(mock.Object);
            mock.Setup(x => x.GetAuthor(1)).Returns(author);
            
            //Act
            var authorController = new AuthorController(mock.Object);
            UnitTestHelper.SetupControllerForTests(authorController);
            var result = authorController.Get(1); //J. K. Rowling
            AuthorDTO responseAuthorDto;
            result.TryGetContentValue(out responseAuthorDto);
            
            //Result
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
