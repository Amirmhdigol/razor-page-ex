using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs;
using RazorEX.BAL.Utilities;

namespace razor_page_ex.Pages.Register
{
    [BindProperties]
    public class SignupModel : PageModel
    {
        public readonly ISignup _signup;
        public SignupModel(ISignup signup)
        {
            _signup = signup;
        }

        [Required]
        [MaxLength(25, ErrorMessage = "Name cannot be greater than 25")]
        [MinLength(4, ErrorMessage = "Name cannot be less than 4")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Range(6,20,ErrorMessage ="Enter between 6 and 20 character")]
        public int Password { get; set; }

        /// <summary>
        /// the model state check these data annotations Modelstate.IsValid down here
        /// </summary>
        [Required] 
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public int RepeatPassword { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            var Result = _signup.Register(new SignupDTO()
            {
                UserName = UserName,
                Password = Password,
                RePassword = RepeatPassword
            });
            if (Result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("UserName", Result.Message);
                return Page();

            }
            return RedirectToPage("../Index");

        }
    }
}
