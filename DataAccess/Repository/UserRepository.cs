using BusinessObject.Models;
using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _userDao;

        public UserRepository(UserDAO userDao)
        {
            _userDao = userDao;
        }

        public User? Authenticate(string email, string password)
        {
            return _userDao.Authenticate(email, password);
        }

        public User GetById(int id)
        {
            return _userDao.GetById(id);
        }

        public void Update(User user)
        {
            _userDao.Update(user);
        }
    }

}
