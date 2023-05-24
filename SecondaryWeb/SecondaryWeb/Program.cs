using SecondaryWeb.Models;
using SecondaryWeb.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAntiforgery(opts => opts.Cookie.Name = "_fc");

var app = builder.Build();

app.MapControllers();

app.MapGet("/payload", async (HttpContext context) =>
{
    StringBuilder sb = new StringBuilder();

    string? cookie = context.Request.Cookies["_id"];
    if (cookie == null)
    {
        sb.Append("N/A");
    }
    else
    {
        JWT jwt = new JWT();
        JWTService jwtService = new JWTService(jwt.SECRET_KEY);

        sb.Append(string.Join(Environment.NewLine, jwtService.GetTokenClaims(cookie)));
    }

    return sb.ToString();
});

app.Run();
