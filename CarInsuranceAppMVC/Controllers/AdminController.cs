using CarInsuranceAppMVC.Models;
using CarInsuranceAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceAppMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (InsuranceCoEntities db = new InsuranceCoEntities())
            {
                //var signups = db.SignUps.Where(x => x.Removed == null).ToList();

                var customerInfoVms = new List<CustomerInfoVm>();

                foreach (var customer in db.CustomerInfoes)
                {
                    var customerInfoVm = new CustomerInfoVm
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        EmailAddress = customer.EmailAddress,
                        QuoteIssued = customer.QuoteIssued
                    };
                    customerInfoVms.Add(customerInfoVm);
                }

                return View(customerInfoVms);
            }
        }
    }
}