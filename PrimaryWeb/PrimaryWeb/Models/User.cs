using Microsoft.AspNetCore.Mvc;

namespace PrimaryWeb.Models
{
    public class User
    {
        public int Id { get; set; }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;
    }
}
