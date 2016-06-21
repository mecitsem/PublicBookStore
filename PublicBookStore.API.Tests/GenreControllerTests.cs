using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using PublicBookStore.API.Controllers;
using PublicBookStore.API.DTOs;
using PublicBookStore.API.Interfaces;
using PublicBookStore.API.Models;

namespace PublicBookStore.API.Tests
{
    [TestFixture]
    public class GenreControllerTests
    {
        private IUnityContainer _container;
        private Mock<IGenreRepository> mock;
        [SetUp]
        public void Setup()
        {
            _container = new UnityContainer();
            mock = new Mock<IGenreRepository>();
            _container.RegisterInstance(mock.Object);
        }

        /// <summary>
        /// GET Genres /api/genre
        /// </summary>
        [Test]
        public void GetGenres_ShouldReturnAllGenres()
        {
            //Arrange
            var genres = SetupGenres();

            if (genres == null) Assert.Fail("Genres collection is null");

            //Create Mock
            mock.Setup(m => m.GetGenres()).Returns(genres);

            var genreController = new GenreController(mock.Object);
            UnitTestHelper.SetupControllerForTests(genreController);

            //Act
            var result = genreController.Get();
            IEnumerable<GenreDTO> responseGenreDtos;
            result.TryGetContentValue(out responseGenreDtos);

            //Assert
            Assert.IsNotNull(responseGenreDtos);
            Assert.IsTrue(responseGenreDtos.Any());
        }

        /// <summary>
        /// Get Book /api/genre/1
        /// </summary>
        [Test]
        public void GetGenre_ShouldReturnFirstGenre()
        {
            //Genre
            var genre = SetupGenres().FirstOrDefault(s => s.GenreId.Equals(1));
            if (genre == null) Assert.Fail("Genre is null");

            //Create Mock
            mock.Setup(m => m.GetGenre(1)).Returns(genre);

            //Act
            var genreController = new GenreController(mock.Object);
            UnitTestHelper.SetupControllerForTests(genreController);
            var result = genreController.Get(1); //Drama
            GenreDTO responseGenreDto;
            result.TryGetContentValue(out responseGenreDto);

            //Assert
            Assert.IsNotNull(responseGenreDto);
            Assert.AreSame(genre.Name, responseGenreDto.Name);
        }

        #region Private member methods

        private static IEnumerable<Genre> SetupGenres()
        {
            var genres = new List<Genre>
            {
               new Genre()
               {
                  GenreId = 1,
                  Name = "Drama"
               },
               new Genre()
               {
                   GenreId = 2,
                   Name = "Classic"
               },
               new Genre()
               {
                   GenreId = 3,
                   Name = "Fable"
               },
               new Genre()
               {
                   GenreId = 4,
                   Name = "Fantasy"
               },
               new Genre()
               {
                   GenreId = 5,
                   Name = "Fanfiction"
               }

            };
            return genres;
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
