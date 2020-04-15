using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Validation
{
    public class BookNameValidation
    {
        private bool numbersAllowed = false;

        public bool IsValid(string name)
        {
            if(name== "Book Name")
            {
                throw new ArgumentException();
            }

            if(numbersAllowed == false && name.Any(char.IsDigit))
            {
                return false;
            }

            return true;
        }
    }
}
