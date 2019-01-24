using CarInsuranceAppMVC.Models;
using CarInsuranceAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceAppMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insurancequote(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, int carYear, string carMake, string carModel, string dui, int tickets, string coverage)
        {
            using (InsuranceCoEntities db = new InsuranceCoEntities())
            {
                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
                else
                {
                    var customerInfo = new CustomerInfo
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        EmailAddress = emailAddress,
                        DoB = dateOfBirth,
                        CarYear = carYear,
                        CarMake = carMake,
                        CarModel = carModel,
                        DUI = dui,
                        Tickets = tickets,
                        Coverage = coverage
                    };

                    int? Quote = 50;
                    int ageDate = dateOfBirth.Year;
                    if (ageDate - 1996 <= 25 || ageDate - 1919 >= 100)
                    {
                        Quote += 25;
                    }
                    else if (ageDate - 2001 <= 18)
                    {
                        Quote += 100;
                    }

                    if ( carYear < 2000 || carYear > 2015)
                    {
                        Quote += 25;
                    }

                    if (carMake == "Porsche")
                    {
                        Quote += 25;
                        if (carMake == "911 Carrera")
                        {
                            Quote += 25;
                        }
                    }
                    Quote += (tickets * 10);

                    if (string.IsNullOrEmpty(dui))
                    {

                    }
                    else
                    {
                        Quote += (Quote / 4);
                    }
                    if (string.IsNullOrEmpty(coverage))
                    {

                    }
                    else
                    {
                        Quote += (Quote / 2);
                    }
                    customerInfo.QuoteIssued = Quote;
                    db.CustomerInfoes.Add(customerInfo);
                    db.SaveChanges();

                    string ShowQuote = Quote.ToString();
                    ViewBag.quote = ShowQuote;
                    return View("Success");
                }
            }
        }
    }
}