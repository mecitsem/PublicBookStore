using AutoMapper;
using PublicBookStore.API.Models;
using PublicBookStore.API.Repositories;
using PublicBookStore.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicBookStore.UI.Web.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int? id)
        {
            var bookModel = new BookModel();
            ViewBag.IsExist = id.HasValue;
            if (id.HasValue)
            {
                bookModel.BookId = (int)id;
            }
            return View(bookModel);
        }

    }
}