using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAssignment.Models;

namespace TestAssignment.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ProductContext db = new ProductContext();
        public ActionResult Index()
        {
            ViewBag.VAT = Convert.ToDecimal(ConfigurationManager.AppSettings["VAT"]);
            return View(db.Products);
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();

            db.ProductAudits.Add(
                new ProductAudit { Action = "add", ProductId = product.Id, UpdateDate = DateTime.Now, UserId = 1 });
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound","Home");
            }
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                return View(product);
            }
            return RedirectToAction("NotFound", "Home");
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            db.ProductAudits.Add(
                new ProductAudit { Action = "edit", ProductId = product.Id, UpdateDate = DateTime.Now, UserId = 1 });
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            db.ProductAudits.Add(
                new ProductAudit { Action = "delete", ProductId = product.Id, UpdateDate = DateTime.Now, UserId = 1 });
            db.SaveChanges();

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ProductAudit(DateTime from, DateTime to)
        {
            var data = db.ProductAudits.Where(x => x.UpdateDate >= from && x.UpdateDate <= to).ToList();
            return View(data);
        }

        public JsonResult GetProductAudit(DateTime from, DateTime to)
        {

            return Json(db.ProductAudits.Where(x=>x.UpdateDate >= from && x.UpdateDate <= to), JsonRequestBehavior.AllowGet);
        }
    }
}