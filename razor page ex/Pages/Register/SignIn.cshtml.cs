using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs;

namespace razor_page_ex.Pages.Register
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class SignInModel : PageModel
    {
        private readonly ISIgnIn _sIgnIn;

        public SignInModel(ISIgnIn SIgnIn)
        {
            _sIgnIn = SIgnIn;
        }

        [Required(ErrorMessage ="REQUIRED")]
        [MaxLength(25, ErrorMessage = "Name cannot be greater than 25")]
        [MinLength(4, ErrorMessage = "Name cannot be less than 4")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [DataType(DataType.Password)]
        [MaxLength(25, ErrorMessage = "password cannot be greater than 25")]
        [MinLength(6, ErrorMessage = "password cannot be less than 6")]
        public string Password { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var user = _sIgnIn.SignIn(new SignInDTO()
            {
                Password = Password,
                UserName = UserName
                
            }); 

            if (user == null)
            {
                ModelState.AddModelError("UserName", "کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }

            List<Claim> claims = new()
            {
                new Claim("test", "test"),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            var Identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(Identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true 
            };
            HttpContext.SignInAsync(principal,properties);

            return RedirectToPage("../Index");

        }

    }
}
