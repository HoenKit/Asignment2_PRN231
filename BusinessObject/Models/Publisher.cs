using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Publisher
    {
        [Key]
        public int pub_id { get; set; }
        public string publisher_name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        [JsonIgnore]
        public ICollection<User>? Users { get; set; } = new List<User>();
        [JsonIgnore]
        public ICollection<Book>? Books { get; set; } = new List<Book>();
    }
}
