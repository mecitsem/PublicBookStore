using Microsoft.Practices.Unity;
using PublicBookStore.API.Interfaces;
using PublicBookStore.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PublicBookStore.API.Initializer
{
    /// <summary>
    /// Register all repositories for Dependency Injection IoC by Unity Container
    /// </summary>
    public class Bootstrapper
    {
        public static void Initialise(HttpConfiguration config)
        {
            var container = new UnityContainer();

            //AuthorRepository
            container.RegisterType<IAuthorRepository, AuthorRepository>(new HierarchicalLifetimeManager());

            //BookRepository
            container.RegisterType<IBookRepository, BookRepository>(new HierarchicalLifetimeManager());

            //GenreRepository
            container.RegisterType<IGenreRepository, GenreRepository>(new HierarchicalLifetimeManager());

            //OrderRepository
            container.RegisterType<IOrderRepository, OrderRepository>(new HierarchicalLifetimeManager());

            //StoreRepository
            container.RegisterType<IStoreRepository, StoreRepository>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

        }


    }
}