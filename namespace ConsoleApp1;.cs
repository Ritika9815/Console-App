namespace ConsoleApp1;

public class Class1
{
    
}
using System;
using System.Linq;
using System.Web.Mvc;
using YourProject.Models;

public class InsureeController : Controller
{
    private YourDbContext db = new YourDbContext();

    // GET: Insuree/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Insuree/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,Age,CarYear,CarMake,CarModel,SpeedingTickets,DUI,FullCoverage")] Insuree insuree)
    {
        if (ModelState.IsValid)
        {
            insuree.Quote = CalculateQuote(insuree);
            db.Insurees.Add(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(insuree);
    }

    private decimal CalculateQuote(Insuree insuree)
    {
        // Start with the base rate of $50
        decimal quote = 50;

        // Age-based logic
        if (insuree.Age <= 18)
        {
            quote += 100;
        }
        else if (insuree.Age >= 19 && insuree.Age <= 25)
        {
            quote += 50;
        }
        else if (insuree.Age >= 26)
        {
            quote += 25;
        }

        // Car year-based logic
        if (insuree.CarYear < 2000)
        {
            quote += 25;
        }
        else if (insuree.CarYear > 2015)
        {
            quote += 25;
        }

        // Car make and model-based logic
        if (insuree.CarMake.ToLower() == "porsche")
        {
            quote += 25;

            if (insuree.CarModel.ToLower() == "911 carrera")
            {
                quote += 25; // Additional $25 for Porsche 911 Carrera
            }
        }

        // Speeding tickets
        quote += insuree.SpeedingTickets * 10;

        // DUI check, add 25% if true
        if (insuree.DUI)
        {
            quote += quote * 0.25m;
        }

        // Full coverage, add 50% if true
        if (insuree.FullCoverage)
        {
            quote += quote * 0.50m;
        }

        return quote;
    }
}
