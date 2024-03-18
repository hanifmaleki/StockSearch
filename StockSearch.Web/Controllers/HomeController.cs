using Newtonsoft.Json;
using StockSearch.Core.Domain;
using StockSearch.Web.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StockSearch.Web.Controllers;

public class HomeController: Controller 
{
    private static string API_URL = "https://ps-async.fekberg.com/api/stocks";

    public async Task<IActionResult> Index()
    {
        using (var client = new HttpClient())
        {
            var responseTask = client.GetAsync($"{API_URL}/MSFT");

            var response = await responseTask;

            var content = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<IEnumerable<StockPrice>>(content);

            return View(data);
        }

        return View();
    }

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

