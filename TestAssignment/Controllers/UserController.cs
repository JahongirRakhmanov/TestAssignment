using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAssignment.Models;

namespace TestAssignment.Controllers
{
    public class UserController : Controller
    {
        ProductContext db = new ProductContext();
        public ActionResult Index()
        {
            ViewBag.VAT = Convert.ToDecimal(ConfigurationManager.AppSettings["VAT"]);
            return View(db.Products);
        }
    }
}