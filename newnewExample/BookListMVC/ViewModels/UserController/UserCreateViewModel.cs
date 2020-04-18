using BookListMVC.Models.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.ViewModels.UserController
{
    public class UserCreateViewModel
    {

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceeed 50 characters")]
        public string Name { get; set; }

        [Required]
        // ? means can be nullable
        public Dept? Department { get; set; }

        [Required]
        //[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
        //    ErrorMessage="Invalid Email Format")]
        [Display(Name = "Office Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Format2")]
        public string Email { get; set; }

        public IFormFile Photo { get; set; }


    }
}
