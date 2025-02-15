using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string email_address { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public int role_id { get; set; }
        public int pub_id { get; set; }
        public DateTime hire_date { get; set; }
        public Role? Role { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
