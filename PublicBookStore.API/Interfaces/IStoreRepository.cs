using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.Interfaces
{
    public interface IStoreRepository : IDisposable
    {
        IEnumerable<Book> GetBooks();
        Book GetBook(int bookId);
        IEnumerable<Book> GetBooksByGenreId(int genreId);
        void SaveChanges();
    }
}
