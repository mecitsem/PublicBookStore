using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;

namespace PublicBookStore.API.Interfaces
{
    public interface IAuthorRepository : IDisposable
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthor(int id);
        Author AddOrUpdate(Author author);
        void Delete(int id);
        void SaveChanges();
    }
}
