using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrimaryWeb.Models;
using PrimaryWeb.Services;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PrimaryWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        internal readonly string URL = "http://localhost:5111";

        public IndexModel(ILogger<IndexModel> logger)
        {
            this.logger = logger;
        }

        public void OnGet()
        {
        }

        public ActionResult OnPostLogin(User user)
        {
            using var hash = SHA256.Create();
            JWT jwt = new JWT()
            {
                Claims = new Claim[]
                {
                    new Claim(
                        "id",
                        Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(user.Email))).ToLower()
                    ),
                }
            };
            JWTService jwtService = new JWTService(jwt.SECRET_KEY);

            HttpContext.Response.Cookies.Append(
                "_id",
                jwtService.GenerateToken(jwt),
                new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                }
            );

            return RedirectToAction("Index");
        }

        public ActionResult OnGetLogout()
        {
            return Logout();
        }

        public ActionResult OnPostLogout()
        {
            return Logout();
        }

        private ActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(
                "_id",
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Secure = true,
                }
            );

            return RedirectToAction("Index");
        }
    }
}