using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Role
    {
        public int role_id { get; set; }
        public string role_desc { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
