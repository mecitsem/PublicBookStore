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
    public class GenreRepository : IGenreRepository, IDisposable
    {
        private PublicBookStoreEntities context;

        public GenreRepository()
        {
            this.context = new PublicBookStoreEntities();
        }

        public Genre AddOrUpdate(Genre genre)
        {
            Genre result = null;
            if (context.Genres.Any(g => g.GenreId.Equals(genre.GenreId)))
            {
                var exGenre = context.Genres.Find(genre.GenreId);
                exGenre.Description = genre.Description;
                exGenre.Name = genre.Name;
                context.Entry(exGenre).State = System.Data.Entity.EntityState.Modified;
                result = exGenre;
            }
            else
                result = context.Genres.Add(genre);

            return result;
            
        }

        public void Delete(int id)
        {
            var genre = context.Genres.Find(id);
            context.Genres.Remove(genre);
        }

        public Genre GetGenre(int id)
        {
            return context.Genres.Find(id);
        }

        public IEnumerable<Genre> GetGenres()
        {
            return context.Genres.ToList();
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
