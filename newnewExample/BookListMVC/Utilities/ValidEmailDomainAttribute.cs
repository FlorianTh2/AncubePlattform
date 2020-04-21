using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        // field = private readonly string allowedDomain
        // property= public int MyProperty {get,set;}
        private readonly string allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }


        // we dont need to include the error message parameter, this
        // parameter gets handeled by base class
        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split("@");
            return strings[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
