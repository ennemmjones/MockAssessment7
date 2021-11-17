using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MockAssessment7.Models;
using Newtonsoft.Json;

namespace MockAssessment7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
            
        }

        public async Task<IActionResult> Search(int id)
        {            
            string url = "https://grandcircusco.github.io/demo-apis/donuts.json";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string jsonContentAsString = await response.Content.ReadAsStringAsync(); 
                var output = JsonConvert.DeserializeObject<FullAPI>(jsonContentAsString);

                
                var donutAPI = output.Results.Find(d => d.Id == id);
                var response2 = await client.GetAsync(donutAPI.Ref);
                string jsonContentAsString2 = await response2.Content.ReadAsStringAsync();

                var donut = JsonConvert.DeserializeObject<Donut>(jsonContentAsString2);

                return View(donut);
            }

        }

        ////cheater method
        //public async Task<IActionResult> Search(int id)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string url = "https://grandcircusco.github.io/demo-apis/donuts/";
        //        HttpResponseMessage message = await client.GetAsync($"{url}{id}.json");

        //        var model = JsonConvert.DeserializeObject<Donut>(await message.Content.ReadAsStringAsync());

        //        return View(model);
        //    }
        //}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
