namespace PublicBookStore.API.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PublicBookStore.API.Data.PublicBookStoreEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PublicBookStore.API.Data.PublicBookStoreEntities context)
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

            genres.ForEach(g => context.Genres.AddOrUpdate(g));
            context.SaveChanges();

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
            authors.ForEach(a => context.Authors.AddOrUpdate(a));
            context.SaveChanges();

            //BOOKS
            
        }
    }
}
