using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using investrs.Models;
using System.Net.Http;
using investrs.Data;
using System.Threading;
using System.IO;

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


        private static HttpClient client = new HttpClient();

        // Precondition: Categories haveb been added to database, with Misc set as 38
        // Query: http://services.runescape.com/m=itemdb_rs/api/catalogue/detail.json?item=21787
        public async Task<IActionResult> UpdateItem()
        {
            // Get item IDs from static file
            string rs3itemjson = System.IO.File.ReadAllText(@"..\investrs\wwwroot\data\items_rs3.json"); // Items from static list (may need to be updated)
            List<string> itemListString = DataRs3Item.FromJson(rs3itemjson).Where(x => x.Value.Tradeable == true).Select(x => x.Key).ToList(); // tradeable items
            List<int> itemListIDs = new List<int>(); 
            foreach(string i in itemListString)
            {
                itemListIDs.Add(Convert.ToInt32(i));
            }
            // -------

            client.BaseAddress = new Uri("http://services.runescape.com/m=itemdb_rs/api/"); // API Address
            int itemsAddedCounter = 0; // Number of items added

            // TODO: Check if GE version is current version
            foreach(int itemID in itemListIDs)
            {

                try
                {
                    // To do: Wait here
                    Thread.Sleep(5001);

                    // Query API with itemID
                    var response = await client.GetAsync("catalogue/detail.json?item=" + itemID.ToString()); // Query string
                    response.EnsureSuccessStatusCode(); // Ensure successful response
                    var stringResult = await response.Content.ReadAsStringAsync(); // Get string from JSON returned
                    DataItem result = DataItem.FromJson(stringResult); // Deserialize JSON string to DataItem object

                    // Create Item class object from JSON
                    Item newItem = new Item()
                    {
                        ApiID = Convert.ToInt32(result.Item.Id),
                        Description = result.Item.Description,
                        Icon = result.Item.Icon,
                        IconLarge = result.Item.IconLarge,
                        Name = result.Item.Name,
                        IsMembersItem = bool.Parse(result.Item.Members),
                        CategoryID = db.Category.Where(x => x.Name == result.Item.Type).FirstOrDefault().Number,
                    };
                    // Check if item exists in database
                    IEnumerable<Item> currentItems = db.Item; // Get items in db
                    if (!currentItems.Select(x => x.ApiID).Contains(newItem.ApiID)) // See if newItemID exists
                    {
                        // Add item to database
                        // Validate
                        if (newItem.CategoryID == 0) // Check if the new item is of category "Misc", which has an API ID of 0, but is stored as 38 in this database
                            newItem.CategoryID = 38; // Chaneg Category ID to 38
                        // Add
                        db.Item.Add(newItem);
                        db.SaveChanges();

                        itemsAddedCounter++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }               
            }
            ViewBag.DataUpdateItemListCounter = itemsAddedCounter;
            return View();
        }

        public async Task<IActionResult> UpdateItemPriceHistory()
        {
            client.BaseAddress = new Uri("http://services.runescape.com/m=itemdb_rs/api/"); // API Address
            List<Item> items = db.Item.Where(x=>x.ItemID > 473).ToList();
            foreach(Item i in items)
            {
                // To do: Wait here
                Thread.Sleep(5001);

                // Query API with itemID
                var response = await client.GetAsync("graph/" + i.ApiID.ToString() + ".json"); // Query string
                response.EnsureSuccessStatusCode(); // Ensure successful response
                var stringResult = await response.Content.ReadAsStringAsync(); // Get string from JSON returned
                DataItemPriceHistory result = DataItemPriceHistory.FromJson(stringResult); // Deserialize JSON string to Data Price History object

                foreach(var daily in result.Daily) // Loop through each daily price in history
                {
                    var average = result.Average.Where(x => x.Key == daily.Key).FirstOrDefault(); // get average price for the day
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(daily.Key)); // get datetime from timestamp
                    DateTime itemHistoryDate = dateTimeOffset.UtcDateTime;
                    string itemHistoryDayOfWeek = dateTimeOffset.DayOfWeek.ToString(); // get day of week from timestamp

                    ItemPriceHistory itemPriceHistory = new ItemPriceHistory() // create new ItemPriceHistory object
                    {
                        ItemID = i.ItemID,
                        Date = itemHistoryDate,
                        DailyPrice = Convert.ToInt32(daily.Value),
                        AveragePrice = Convert.ToInt32(average.Value),
                        DayOfWeek = itemHistoryDayOfWeek
                    };

                    int itemPriceHistoryExist = db.ItemPriceHistory.Where(x => x.ItemID == i.ItemID).Where(x => x.Date == itemHistoryDate).Count();
                    if (itemPriceHistoryExist == 0)
                    {
                        db.ItemPriceHistory.Add(itemPriceHistory);
                        db.SaveChanges();
                    }

                }
            }

            


            return View();
        }
    }
}