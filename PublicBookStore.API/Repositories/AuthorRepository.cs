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
    public class AuthorRepository : IAuthorRepository, IDisposable
    {
        private PublicBookStoreEntities _context;

        public AuthorRepository()
        {
            this._context = new PublicBookStoreEntities();
        }

        public virtual Author AddOrUpdate(Author author)
        {
            Author result = null;
            if (_context.Authors.Any(a => a.AuthorId.Equals(author.AuthorId)))
            {
                var exAuthor = _context.Authors.FirstOrDefault(a => a.AuthorId.Equals(author.AuthorId));
                if(exAuthor == null)
                    throw new ArgumentNullException(nameof(author));

                exAuthor.Name = author.Name;
                _context.Entry(author).State = System.Data.Entity.EntityState.Modified;

                result = exAuthor;
            }
            else
                result = _context.Authors.Add(author);

            return result;
        }

        public virtual void Delete(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.AuthorId.Equals(id));
            _context.Authors.Remove(author);
        }

        public virtual Author GetAuthor(int id)
        {
            return _context.Authors.Find(id);
        }

        public virtual IEnumerable<Author> GetAuthors()
        {
            return _context.Authors.ToList();
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool _disposed;
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
