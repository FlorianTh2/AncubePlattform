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
                new User() {Id=1, Name="Mary", Department="HR", Email="test@test.com"},
                new User() {Id=2, Name="Mary2", Department="IT", Email="test2@test.com"},
                new User() {Id=3, Name="Mary3", Department="IT", Email="test3@test.com"},
            };

        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userList;
        }

        public User GetUser(int Id)
        {
            return _userList.FirstOrDefault(a => a.Id == Id);
        }
    }
}
