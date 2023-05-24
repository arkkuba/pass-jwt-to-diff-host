using System.Linq;
using System.Net.Http;
using System.Text;
using System;
using Microsoft.AspNetCore.Mvc;

namespace SecondaryWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly string URL = "http://localhost:5166";

        [HttpGet("")]
        public ActionResult Index()
        {
            string? cookie = HttpContext.Request.Cookies["_id"];

            return View("Index", cookie);
        }

        [HttpPost("")]
        public ActionResult Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                HttpContext.Response.Cookies.Append(
                    "_id",
                    id,
                    new CookieOptions()
                    {
                        HttpOnly = true,
                        Secure = true,
                    }
                );
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(
                "_id",
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Secure = true,
                }
            );

            return Redirect($"{URL}/?handler=Logout");
        }
    }
}
