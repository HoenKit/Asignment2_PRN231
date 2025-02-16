using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Book
    {
        [Key]
        public int book_id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public int pub_id { get; set; }
        public float price { get; set; }
        public string advance {  get; set; }
        public string royalty { get; set; }
        public float ytd_sales {  get; set; }
        public string notes { get; set; }
        public DateTime published_date { get; set; }
        [JsonIgnore]
        public Publisher? Publisher { get; set; }
        [JsonIgnore]
        public ICollection<BookAuthor>? BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
