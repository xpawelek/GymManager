using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GymManagerWebApp.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task OnGetAsync()
        {
            // Wylogowanie (usuniecie cookie)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Po wylogowaniu wróć na stronę główną
            Response.Redirect("/");
        }
    }
}
