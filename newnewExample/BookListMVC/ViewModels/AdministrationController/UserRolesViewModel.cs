using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.ViewModels.AdministrationController
{
    public class UserRolesViewModel
    {
        // here again we actually need userid too but because
        // user id does not change with each role we would store
        // user id redundant, so lets pass userid via ViewBag
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
