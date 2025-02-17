using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class UserDAO
    {
        private readonly eBookStoreContext _context;

        public UserDAO(eBookStoreContext context)
        {
            _context = context;
        }

        public User? Authenticate(string email, string password)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.email_address == email && u.password == password);
        }
    }

}
