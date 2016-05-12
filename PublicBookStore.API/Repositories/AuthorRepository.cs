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

        public void AddOrUpdate(Author author)
        {
            if (context.Authors.Contains(author))
                context.Entry(author).State = System.Data.Entity.EntityState.Modified;
            else
                context.Authors.Add(author);
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
