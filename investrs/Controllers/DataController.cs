using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using investrs.Models;
using System.Net.Http;
using investrs.Data;

namespace investrs.Controllers
{
    public class DataController : Controller
    {
        // Db Context
        private readonly ApplicationDbContext db;

        public DataController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SyncItemPriceHistory()
        {
            // Query: http://services.runescape.com/m=itemdb_rs/api/catalogue/detail.json?item=21787

            int MaxItemID = 99999;

            using(var client = new HttpClient())
            {
                for (int i = 21787; i < MaxItemID; i++)
                {
                    try
                    {
                        client.BaseAddress = new Uri("http://services.runescape.com/m=itemdb_rs/api/");
                        var response = await client.GetAsync("catalogue/detail.json?item=" + i.ToString());
                        response.EnsureSuccessStatusCode();

                        var stringResult = await response.Content.ReadAsStringAsync();
                        DataItem result = DataItem.FromJson(stringResult);
                    }
                    catch(Exception e)
                    {

                    }
                }

            }


            return View();
        }


    }
}