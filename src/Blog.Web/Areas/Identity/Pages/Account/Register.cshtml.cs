using Blog.Application.Interfaces;
using Blog.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthenticationApplicationServices _authenticationApplication;

        public RegisterModel(IAuthenticationApplicationServices authenticationApplication)
        {
            _authenticationApplication = authenticationApplication;
        }

        [BindProperty]
        public RegisterUserModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
       
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _authenticationApplication.RegisterAuthorAsync(Input);
                if (result.Succeeded)
                    return LocalRedirect(returnUrl);
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Page();
        }
    }
}
