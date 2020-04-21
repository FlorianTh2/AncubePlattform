using BookListMVC.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.ViewModels.UserController.identity
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        // Add remote validation
        // under the hood it uses ajax to call the given action to validate the given field
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain: "gmail.com", ErrorMessage ="Email domain must be gmail.com")]
        public string Email{ get; set; }

        public string City { get; set; }

        [Required]
        // to mask the given password with e.g. asterisks
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password dod not match.")]
        public string ConfirmPassword { get; set; }



    }
}
