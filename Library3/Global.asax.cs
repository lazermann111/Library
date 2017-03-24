using Library3.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

            if (true)
            {
                DbHelper.GenerateMongoDbContent();
                DbHelper.GeneratePostgresContent();
            }

            int workerThreads;
            int completionPortThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine("workerThreads " + workerThreads);
            Console.WriteLine(completionPortThreads);

            Console.WriteLine(System.Diagnostics.Process.GetCurrentProcess().Threads.Count); ;
        }
    }
}
