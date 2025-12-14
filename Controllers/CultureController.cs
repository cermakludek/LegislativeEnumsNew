using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace LegislativeEnumsNew.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CultureController : ControllerBase
{
    [HttpGet("Set")]
    public IActionResult Set(string culture, string returnUrl = "/")
    {
        if (!string.IsNullOrEmpty(culture))
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax
                }
            );
        }

        // Sanitize return URL - for API controller we just redirect
        if (string.IsNullOrEmpty(returnUrl))
        {
            returnUrl = "/";
        }

        return Redirect(returnUrl);
    }
}
