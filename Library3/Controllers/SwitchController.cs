using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library3.Controllers
{
    public class SwitchController : ApiController
    {

        public void Switch()
        {
            WebApiApplication.MongoDbUsed = !WebApiApplication.MongoDbUsed;
        }
    }
}
