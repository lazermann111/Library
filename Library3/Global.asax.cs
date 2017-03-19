using Library3.Helpers;
using Newtonsoft.Json;
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
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configure(WebApiConfig.Register);

            AutomapperConfiguration.Configure();
            DbHelper.GenerateMongoDbContent();
            DbHelper.GeneratePostgresContent();
        }
    }
}
