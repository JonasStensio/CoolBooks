using CoolBooks.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CoolBooks
{
    public class MvcApplication: System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SearchBook("9780547928197");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);        
        }

        public static string SearchBook(string isbn)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49905");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn).Result;

            string jsonData = response.Content.ReadAsStringAsync().Result;

            var res = JsonConvert.DeserializeObject<Books>(jsonData);
            Debug.WriteLine(jsonData);
            return jsonData; // return book;
            
        }


    }
}
