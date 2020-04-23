using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.ViewModels.AdministrationController
{
    public class UserRoleViewModel
    {
        // with the view we want to make the user select if a user should be added
        // or removed from this role, so we need to identify this user across we get-edit, view-edit
        // and post-edit to relate a specific user given in a specific role to the check-box:
        // ~"should be in this role or not"
        // to make this relation of should be in this role or not we need userid and roleid and
        // isSelected and eventually for user-readability Username
        // because this view is intendet to be shown like that: -role is initially displayed with
        // other roles -> there i can select "Edit" on 1 Role- we show users only for 1 role
        // and thats why we can but should not store userrole in this rolemodel since its only 1
        // role and not for every user an other role and this roleid can be given to the
        // view (only to send this id back to post-action later) by ViewBag
        // e.g. ViewBag.roleId = ...
        // thats why we can ommit the roleid property here

        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
