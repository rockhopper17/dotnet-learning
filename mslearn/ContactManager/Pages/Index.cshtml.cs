using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ContactManager.Pages;

[AllowAnonymous]
public class IndexModel : PageModel
{
    public void OnGet()
    {

    }
}
