using PublicBookStore.API.Data;
using PublicBookStore.API.Interfaces;
using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.Repositories
{
    public class StoreRepository : IStoreRepository, IDisposable
    {
        private PublicBookStoreEntities context;

        public StoreRepository()
        {
            this.context = new PublicBookStoreEntities();
        }

        //Collecitons
        public IEnumerable<Book> GetBooks()
        {
            return context.Books.ToList();
        }

        public Book GetBook(int id)
        {
            return context.Books.Find(id);
        }

        public IEnumerable<Book> GetBooksByGenreId(int genreId)
        {
            return context.Books.Where(b => b.Genre.GenreId.Equals(genreId)).ToList();
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
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
