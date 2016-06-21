using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;

namespace PublicBookStore.API.Interfaces
{
    public interface IGenreRepository : IDisposable
    {
        IEnumerable<Genre> GetGenres();
        Genre GetGenre(int id);
        Genre AddOrUpdate(Genre genre);
        void Delete(int id);
        void SaveChanges();
    }
}
