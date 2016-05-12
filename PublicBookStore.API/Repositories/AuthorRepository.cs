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
        private PublicBookStoreEntities context;

        public AuthorRepository()
        {
            this.context = new PublicBookStoreEntities();
        }

        public Author AddOrUpdate(Author author)
        {
            Author result = null;
            if (context.Authors.Any(a => a.AuthorId.Equals(author.AuthorId)))
            {
                var exAuthor = context.Authors.Find(author.AuthorId);
                exAuthor.Name = author.Name;
                context.Entry(author).State = System.Data.Entity.EntityState.Modified;
                result = exAuthor;
            }
            else
                result = context.Authors.Add(author);

            return result;
        }

        public void Delete(int id)
        {
            var author = context.Authors.Find(id);
            context.Authors.Remove(author);
        }

        public Author GetAuthor(int id)
        {
            return context.Authors.Find(id);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return context.Authors.ToList();
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
