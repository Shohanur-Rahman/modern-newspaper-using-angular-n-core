using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class TestController : Controller
    {
        public JsonResult Index()
        {
            string endpoint = "https://jsonplaceholder.typicode.com/todos";

            var result = new WebClient().DownloadString(endpoint);
            dynamic json = JsonConvert.DeserializeObject(result);

            return Json(JsonConvert.DeserializeObject(result));
        }
    }
}
