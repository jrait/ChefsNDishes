using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChefsNDishes.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers
{
    
    public class HomeController : Controller
    {
        private Context dbcontext;
        public HomeController(Context context)
        {
            dbcontext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> allChefs = dbcontext.Chefs.ToList();
            ViewBag.Chefs = allChefs;
            return View();
        }
        [HttpGet("new")]
        public IActionResult NewChef()
        {
            return View();
        }
        [HttpPost("create")]
        public IActionResult CreateChef(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                dbcontext.Add(newChef);
                dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewChef");
            
        }

        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<Dish> alldishes = dbcontext.Dishes.Include(d => d.Creator).ToList();
            ViewBag.Dishes = alldishes;
            return View();
        }
        [HttpGet("dishes/new")]
        public IActionResult NewDish()
        {
            List<Chef> allChefs = dbcontext.Chefs.ToList();
            ViewBag.Chefs = allChefs;
            return View();
        }
        [HttpPost("dishes/create")]
        public IActionResult CreateDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                dbcontext.Add(newDish);
                dbcontext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            List<Chef> allChefs = dbcontext.Chefs.ToList();
            ViewBag.Chefs = allChefs;
            return View("NewDish");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
    public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }
        
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        string value = session.GetString(key);
        return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
    }
}
}
