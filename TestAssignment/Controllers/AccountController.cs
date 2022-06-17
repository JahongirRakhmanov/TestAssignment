using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

using TestAssignment.Models;
using TestAssignment.Helpers;
using System.Web.Security;

namespace TestAssignment.Controllers
{
    public class AccountController : Controller
    {
        ProductContext db = new ProductContext();
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string username, string password)
        {
            User user = db.Users.Where(t => t.UserName == username && t.Password == password).FirstOrDefault();
            if (user != null)
            {
                var authTicket = new FormsAuthenticationTicket(
                                        1,                              // version
                                        username,                       // user name
                                        DateTime.Now,                   // created
                                        DateTime.Now.AddMinutes(20),    // expires
                                        false,                          // persistent?
                                        user.Role.RoleName              // 
                                     );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                
                if (user.RoleId == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (user.RoleId == 2)
                {
                    return RedirectToAction("Index", "User");
                }
            }

            ModelState.AddModelError("", "invalid username or password!");
            return View();
        }


        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user, string Password2)
        {
            if (user.Password != Password2)
            {
                ModelState.AddModelError("", "Passwords do not match!");
                return View();
            }
            var data = db.Users.FirstOrDefault(x => x.UserName == user.UserName);
            if (data != null)
            {
                ModelState.AddModelError("", "This Username is already taken!");
                return View();
            }
            var data2 = db.Users.FirstOrDefault(x => x.Email == user.Email);
            if (data2 != null)
            {
                ModelState.AddModelError("", "This Email is already taken!");
                return View();
            }

            user.RoleId = 2;
            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Index", "User");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }
    }
}