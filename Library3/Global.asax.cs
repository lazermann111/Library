using Library3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Library3
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static bool MongoDbUsed = true;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            DbHelper.GenerateMongoDbContent();
            DbHelper.GeneratePostgresContent();
        }
    }
}
