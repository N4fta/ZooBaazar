using Logic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WebApp.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly EmployeeManager employeeManager;
        public ProfileModel(EmployeeManager employeeManager)
        {
            this.employeeManager = employeeManager;
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Birthday { get; set; }

        public void OnGet()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                UserId = userIdClaim != null ? int.Parse(userIdClaim) : 0;
                Name = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                Email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
                Username = claimsIdentity.FindFirst("Username")?.Value;
                PhoneNumber = claimsIdentity.FindFirst("PhoneNumber")?.Value;
                Address = claimsIdentity.FindFirst("Address")?.Value;
                Birthday = claimsIdentity.FindFirst(ClaimTypes.DateOfBirth)?.Value;

            }
        }

        public IActionResult OnPostUpdatePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (oldPassword == null || newPassword == null || confirmPassword == null)
            {
                return RedirectToPage();
            }

            var username = User.FindFirstValue("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToPage();
            }

            var user = employeeManager.GetUser(username);
            if (user == null)
            {
                return RedirectToPage();
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "New password and confirm password do not match");
                return RedirectToPage();
            }

            var result = employeeManager.ChangePassword(user, oldPassword, newPassword);

            return RedirectToPage();
        }
    }
}