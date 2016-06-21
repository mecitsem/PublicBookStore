using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;

namespace PublicBookStore.API.Interfaces
{
    public interface IBookRepository : IDisposable
    {
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBooksByGenreId(int genreId);
        IEnumerable<Book> GetBooksByAuthorId(int authorId);
        Book GetBook(int id);
        IEnumerable<Genre> Genres();
        IEnumerable<Author> Authors();
        Book AddOrUpdate(Book book);
        void Delete(int id);
        void SaveChanges();
    }
}
