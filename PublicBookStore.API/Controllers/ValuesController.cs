using PublicBookStore.API.Data;
using PublicBookStore.API.Interfaces;
using PublicBookStore.API.Models;
using PublicBookStore.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PublicBookStore.API.Controllers
{
    public class ValuesController : ApiController
    {


        // GET api/values
        public string Get()
        {
            return "hello world";
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "hello" + id;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }
    }
}
