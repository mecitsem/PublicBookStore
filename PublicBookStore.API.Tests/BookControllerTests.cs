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
    public class BookControllerTests
    {
        private IUnityContainer _container;
        private Mock<IBookRepository> mock;
        [SetUp]
        public void Setup()
        {
            _container = new UnityContainer();
            mock = new Mock<IBookRepository>();
            _container.RegisterInstance(mock.Object);
        }

        /// <summary>
        /// GET Books /api/book
        /// </summary>
        [Test]
        public void GetBooks_ShouldReturnAllBooks()
        {
            //Arrange
            var books = SetupBooks();

            if (books == null) Assert.Fail("Books collection is null");

            //Create Mock
            mock.Setup(m => m.GetBooks()).Returns(books);

            var bookController = new BookController(mock.Object);
            UnitTestHelper.SetupControllerForTests(bookController);

            //Act
            var result = bookController.Get();
            IEnumerable<BookDTO> resultBooksBookDtos;
            result.TryGetContentValue(out resultBooksBookDtos);

            //Assert
            Assert.IsNotNull(resultBooksBookDtos);
            Assert.IsTrue(resultBooksBookDtos.Any());
        }

        /// <summary>
        /// Get Book /api/book/1
        /// </summary>
        [Test]
        public void GetBook_ShouldReturnFirstBook()
        {
            //Book
            var book = SetupBooks().FirstOrDefault(s => s.BookId.Equals(2));
            if (book == null) Assert.Fail("Book is null");

            //Create Mock
            mock.Setup(m => m.GetBook(2)).Returns(book);

            //Act
            var authorController = new BookController(mock.Object);
            UnitTestHelper.SetupControllerForTests(authorController);
            var result = authorController.Get(2); //The Trials of Apollo
            BookDTO responseBookDto;
            result.TryGetContentValue(out responseBookDto);

            //Assert
            Assert.IsNotNull(responseBookDto);
            Assert.AreSame(book.Title, responseBookDto.Title);
        }

        #region Private member methods

        private static IEnumerable<Book> SetupBooks()
        {
            var books = new List<Book>
            {
               new Book()
               {
                   AuthorId = 33,
                   BookId = 2,
                   GenreId = 9,
                   Title = "The Trials of Apollo",
                   Description = "After angering his father Zeus, the god Apollo is cast down from Olympus. Weak and disoriented, he lands in New York City as a regular teenage boy. Now, without his godly powers, the four-thousand-year-old deity must learn to survive in the modern world until he can somehow find a way to regain Zeus's favor.",
                   ImageUrl = "Content/Books/apollo.jpg",
                   Published = new DateTime(2016,5,3),
                   Price = 11.99m
               },
               new Book()
               {
                   AuthorId = 9,
                   BookId = 3,
                   GenreId = 4,
                   Title = "Me Before You",
                   Description = "Louisa Clark is an ordinary girl living an exceedingly ordinary life—steady boyfriend, close family—who has barely been farther afield than their tiny village. She takes a badly needed job working for ex–Master of the Universe Will Traynor, who is wheelchair bound after an accident. Will has always lived a huge life—big deals, extreme sports, worldwide travel—and now he’s pretty sure he cannot live the ",
                   ImageUrl = "/Content/Books/vampire.jpg",
                   Published = new DateTime(2016,5,26),
                   Price = 6.12m
               },
               new Book()
               {
                   AuthorId = 8,
                   BookId = 7,
                   GenreId = 8,
                   Title = "End of Watch: A Novel",
                   Description = "The spectacular finale to the New York Times bestselling trilogy that began with Mr. Mercedes (winner of the Edgar Award) and Finders Keepers—In End of Watch, the diabolical “Mercedes Killer” drives his enemies to suicide, and if Bill Hodges and Holly Gibney don’t figure out a way to stop him, they’ll be victims themselves.",
                   ImageUrl = "/Content/Books/endofwatch.jpg",
                   Published = new DateTime(2016,6,7),
                   Price = 20.12m
               }
            };
            return books;
        }




        #endregion
    }
}
