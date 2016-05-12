using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.Interfaces
{
    public interface IAuthorRepository : IDisposable
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthor(int id);
        void AddOrUpdate(Author author);
        void Delete(int id);
        void SaveChanges();
    }
}
