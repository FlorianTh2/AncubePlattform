using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models.User
{
    public class MockUserRepository : IUserRepository
    {

        private List<User> _userList;

        public MockUserRepository()
        {
            _userList = new List<User>() {
                new User() {Id=1, Name="Mary", Department=Dept.HR, Email="test@test.com"},
                new User() {Id=2, Name="Mary2", Department=Dept.IT, Email="test2@test.com"},
                new User() {Id=3, Name="Mary3", Department=Dept.IT, Email="test3@test.com"},
            };

        }

        public User Add(User user)
        {
            user.Id = _userList.Max(e => e.Id) + 1;
            _userList.Add(user);
            return user;
        }

        public User Delete(int id)
        {
            User user = _userList.FirstOrDefault(a => a.Id == id);
            if (user != null)
            {
                _userList.Remove(user);
            }
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userList;
        }

        public User GetUser(int Id)
        {
            return _userList.FirstOrDefault(a => a.Id == Id);
        }

        public User Update(User userChanges)
        {
            User user = _userList.FirstOrDefault(a => a.Id == userChanges.Id);
            if(user != null)
            {
                user.Name = userChanges.Name;
                user.Email = userChanges.Email;
                user.Department = userChanges.Department;
            }
            return user;
        }
    }
}
