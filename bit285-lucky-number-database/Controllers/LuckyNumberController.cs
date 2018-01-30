using bit285_lucky_number_database.Models;
using lucky_number_database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bit285_lucky_number_database.Controllers
{
    public class LuckyNumberController : Controller
    {
        private LuckyNumberDbContext dbc = new LuckyNumberDbContext();
        // GET: LuckyNumber
        public ActionResult Spin()
        {
            LuckyNumber myLuck = new LuckyNumber { Number = 7, Balance = 4 };
            dbc.LuckyNumbers.Add(myLuck);
            dbc.SaveChanges();
            return View(myLuck);
        }

        [HttpPost]
        public ActionResult Spin(LuckyNumber lucky)
        {
            int comrade = Convert.ToInt32(Session["comrade"]);
            LuckyNumber databaseLucky = dbc.LuckyNumbers.Where(m=>m.ID == comrade).First();
            //Change the balance in the database
            if(databaseLucky.Balance > 0)
            {
                databaseLucky.Balance -= 1;
            }
            //Update the Number in the Database using the form submission value
            databaseLucky.Number = lucky.Number;
            dbc.SaveChanges();
            return View(databaseLucky);
        }

        //GET Index
        public ActionResult Index()
        {
            return View();
        }

        //POST Index
        
        [HttpPost]
        public ActionResult Index(LuckyNumber userStuff)
        {
            dbc.LuckyNumbers.Add(userStuff);
            dbc.SaveChanges();
            Session["comrade"] = userStuff.ID;
            return View("Spin", userStuff);
        }
        
    }
}