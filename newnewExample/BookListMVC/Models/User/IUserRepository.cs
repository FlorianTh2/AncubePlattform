using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models.User
{
    public interface IUserRepository
    {
        User GetUser(int Id);
        IEnumerable<User> GetAllUsers();
        User Add(User user);
        User Update(User userChanges);
        User Delete(int id);
    }
}
