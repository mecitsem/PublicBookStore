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
        private PublicBookStoreEntities context;

        public BookRepository()
        {
            this.context = new PublicBookStoreEntities();
        }

        public Book AddOrUpdate(Book book)
        {
            Book result;
            if (context.Books.Any(b => b.BookId.Equals(book.BookId)))
            {
                var exBook = context.Books.Find(book.BookId);
                exBook.Author = book.Author;
                exBook.AuthorId = book.AuthorId;
                exBook.Description = book.Description;
                exBook.Genre = book.Genre;
                exBook.GenreId = book.GenreId;
                exBook.ImageUrl = book.ImageUrl;
                exBook.Price = book.Price;
                exBook.Title = book.Title;
                exBook.Published = book.Published;
                context.Entry(book).State = System.Data.Entity.EntityState.Modified;
                result = exBook;
            }
            else
                result = context.Books.Add(book);

            return result;
        }

        public IEnumerable<Author> Authors()
        {
            return context.Authors;
        }

        public void Delete(int id)
        {
            var book = context.Books.Find(id);
            context.Books.Remove(book);
        }

        public IEnumerable<Genre> Genres()
        {
            return context.Genres.ToList();
        }

        public Book GetBook(int id)
        {
            return context.Books.Find(id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return context.Books.ToList();
        }

        public IEnumerable<Book> GetBooksByAuthorId(int authorId)
        {
            return context.Books.Where(b => b.AuthorId.Equals(authorId)).ToList();
        }

        public IEnumerable<Book> GetBooksByGenreId(int genreId)
        {
            return context.Books.Where(b => b.GenreId.Equals(genreId)).ToList();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
