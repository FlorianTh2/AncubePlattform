using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models.User;
using BookListMVC.ViewModels.UserController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BookListMVC.Controllers
{
    [Route("[controller]/[action]")]
    // if we are not logged in, we get redirected to login page out of the page just by this attribute alone
    // we use that at controller-level (at action-level also possible)
    //  -> all actions needs authorization and that mean here (at least for now since we have no roles) only authentification (=login)
    //  -> if we want single actions to be accessible -> [AllowAnonymous]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public UserController(IUserRepository userRepository, IHostingEnvironment hostingEnvironment)
        {
            _userRepository = userRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // string, int, ...
        // JsonResult, ViewResult, RedirectActionResult
        // IActionResult is an interface which is included in all classes above
        // specify default route in this controller (below)
        [Route("~/User")]
        // ~/ ignores controller route
        [AllowAnonymous]
        public ViewResult Index()
        {
            var viewModel = _userRepository.GetAllUsers();
            return View(viewModel);
        }

        // Attribute routing, actually not needed due to our mvc-endpoint routing configured in the startup file
        //[Route("User/Details/{id?}")]
        [Route("{id?}")]
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

            User user = _userRepository.GetUser(id.Value);
            if(user == null)
            {
                Response.StatusCode = 404;
                return View("UserNotFound", id.Value);
            }

            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel()
            {
                // if id null -> default to 1
                User = _userRepository.GetUser(id ?? 1),
                PageTitle = "User Details"
            };
            return View(userDetailsViewModel);
        }

        // returns page with the form
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        //get invoked by the form
        [HttpPost]
        public IActionResult Create(UserCreateViewModel model)
        {
            // it is valid if all required fields (defined in Model) auch filled
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string updoadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(updoadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                }
                User newUser = new User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _userRepository.Add(newUser);

                // since we saved our user by entity framework, entity framework
                // generates an ID for that user and since we still have the user referenced
                // we can just reference to that id
                return RedirectToAction("details", new { id = newUser.Id });
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            User user = _userRepository.GetUser(id);
            UserEditViewModel userEditViewModel = new UserEditViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Department = user.Department,
                ExistingPhotoPath = user.PhotoPath
            };

            return View(userEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(UserEditViewModel model)
        {
            // it is valid if all required fields (defined in Model) auch filled
            if (ModelState.IsValid)
            {
                // where does the Id come from?
                // in the edit (HttpGet) we gave the id along to the view and create a hidden field in the view
                // so thats where our id comes from
                User user = _userRepository.GetUser(model.Id);
                user.Name = model.Name;
                user.Email = model.Email;
                user.Department = model.Department;
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath1 = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath1);
                    }
                    string updoadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(updoadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                    user.PhotoPath = uniqueFileName;
                }
                _userRepository.Update(user);
            }
            return RedirectToAction("details", model.Id);
        }
    }
}