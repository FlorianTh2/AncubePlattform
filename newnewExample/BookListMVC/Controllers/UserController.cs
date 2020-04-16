using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models.User;
using BookListMVC.ViewModels.UserController;
using Microsoft.AspNetCore.Mvc;

namespace BookListMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // string, int, ...
        // JsonResult, ViewResult,
        // IActionResult
        public ViewResult Index()
        {
            var viewModel = _userRepository.GetAllUsers();
            return View(viewModel);
        }

        // Attribute routing, actually not needed due to our mvc-endpoint routing configured in the startup file
        //[Route("User/Details/{id?}")]
        public ViewResult Details(int? id)
        {

            // 1. way
            //ViewData["User"] = user;
            //ViewData["PageTitle"] = "User Details";

            // 2. way
            //ViewBag.User = user;

            // 3. way
            //ViewBag.PageTitle = "User Details";
            //return View(user);

            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel()
            {
                // if id null -> default to 1
                User = _userRepository.GetUser(id??1),
                PageTitle = "User Details"
            };
            return View(userDetailsViewModel);
        }
    }
}