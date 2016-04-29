using PublicBookStore.API.DB;
using PublicBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.Initializer
{
    public class PublicBookStoreSampleData : DropCreateDatabaseIfModelChanges<PublicBookStoreEntities>
    {
        protected override void Seed(PublicBookStoreEntities context)
        {

            //GENRES
            var genres = new List<Genre>
            {
                new Genre { Name = "Drama" },
                new Genre { Name = "Classic" },
                new Genre { Name = "Fable" },
                new Genre { Name = "Fantasy" },
                new Genre { Name = "Fanfiction" },
                new Genre { Name = "Horror" },
                new Genre { Name = "Mythology" },
                new Genre { Name = "Tragedy" },
                new Genre { Name = "Novel" },
                new Genre { Name = "Realistic fiction" }
            };

            //AUTHORS
            var authors = new List<Author>
            {
                new Author { Name = "J. K. Rowling" },
                new Author { Name = "Bella Forrest" },
                new Author { Name = "David Baldacci" },
                new Author { Name = "Nora Roberts" },
                new Author { Name = "Marcia Clark" },
                new Author { Name = "James Patterson" },
                new Author { Name = "Scott Pratt" },
                new Author { Name = "Stephen King" }
            };


            //BOOKS
            new List<Book>
            {
              new Book() { Title="End of Watch", Author = authors.Single(a=>a.Name.Equals("Stephen King")),Genre = genres.Single(g=>g.Equals("Novel")),ImageUrl = "/Content/BookImages/endofwatch.jpg",Published = new DateTime(2016,7,1) }

            }.ForEach(b => context.Books.Add(b));


        }
    }
}
