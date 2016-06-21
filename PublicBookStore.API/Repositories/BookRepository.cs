using PublicBookStore.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicBookStore.API.Models;
using PublicBookStore.API.Data;

namespace PublicBookStore.API.Repositories
{
    public class BookRepository : IBookRepository, IDisposable
    {
        private PublicBookStoreEntities _context;

        public BookRepository()
        {
            this._context = new PublicBookStoreEntities();
        }

        public virtual Book AddOrUpdate(Book book)
        {
            Book result;
            if (_context.Books.Any(b => b.BookId.Equals(book.BookId)))
            {
                var exBook = _context.Books.Find(book.BookId);
                exBook.Author = book.Author;
                exBook.AuthorId = book.AuthorId;
                exBook.Description = book.Description;
                exBook.Genre = book.Genre;
                exBook.GenreId = book.GenreId;
                exBook.ImageUrl = book.ImageUrl;
                exBook.Price = book.Price;
                exBook.Title = book.Title;
                exBook.Published = book.Published;
                _context.Entry(book).State = System.Data.Entity.EntityState.Modified;
                result = exBook;
            }
            else
                result = _context.Books.Add(book);

            return result;
        }

        public virtual IEnumerable<Author> Authors()
        {
            return _context.Authors;
        }

        public virtual void Delete(int id)
        {
            var book = _context.Books.Find(id);
            _context.Books.Remove(book);
        }

        public virtual IEnumerable<Genre> Genres()
        {
            return _context.Genres.ToList();
        }

        public virtual Book GetBook(int id)
        {
            return _context.Books.Find(id);
        }

        public virtual IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public virtual IEnumerable<Book> GetBooksByAuthorId(int authorId)
        {
            return _context.Books.Where(b => b.AuthorId.Equals(authorId)).ToList();
        }

        public virtual IEnumerable<Book> GetBooksByGenreId(int genreId)
        {
            return _context.Books.Where(b => b.GenreId.Equals(genreId)).ToList();
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
