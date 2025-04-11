using Logic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Pages.Models;
using Microsoft.AspNetCore.Authentication;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly EmployeeManager _employeeManager;
        public LoginModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [BindProperty]
        public LoginUser LoginUser { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState["LoginUser.Email"].ValidationState == ModelValidationState.Valid
                && ModelState["LoginUser.Password"].ValidationState == ModelValidationState.Valid)
            {
                List<Employee> employees = _employeeManager.LoadEmployeesFromDataBase();
                Employee user = employees.Find(u => u.Email == LoginUser.Email && u.Password == LoginUser.Password);

                if (user == null)
                {
                    // Email not found OR password incorrect
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return Page();
                }

                List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString("dd/mm/yyyy")),
                new Claim("PhoneNumber", user.PhoneNumber),
                new Claim("Address", user.Address),
                new Claim("Username", user.Username),
                new Claim("Password", user.Password)
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToPage("/Index");
            }
            else
            {

                return Page();
            }
        }
    }    }