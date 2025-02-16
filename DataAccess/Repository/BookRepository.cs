using BusinessObject.Models;
using BusinessObject;
using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(eBookStoreContext context, BookDAO dao)
            : base(context, dao) { }
    }
}
