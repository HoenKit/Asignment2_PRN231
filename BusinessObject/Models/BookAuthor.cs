using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class BookAuthor
    {
        public int author_id { get; set; }
        public int book_id { get; set; }
        public string author_order {  get; set; }
        public float royalty_percentage { get; set; }
        public Book? Book { get; set; }
        public Author? Author { get; set; }
    }
}
