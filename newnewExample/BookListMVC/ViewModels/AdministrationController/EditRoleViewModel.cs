using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.ViewModels.AdministrationController
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            // needed if we call .Any() in view since otherwise
            // we call .Any() on null and that throws an Exception
            Users = new List<string>();
        }

        public string Id { get; set; }
        [Required(ErrorMessage = "Rolename is required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
