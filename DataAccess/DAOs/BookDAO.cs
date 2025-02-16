using BusinessObject.Models;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class BookDAO
    {
        private readonly eBookStoreContext _context;

        public BookDAO(eBookStoreContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            return _context.Books.Include(b => b.Publisher).ToList();
        }

        public Book GetById(int bookId)
        {
            return _context.Books
                .Include(b => b.Publisher)
                .FirstOrDefault(b => b.book_id == bookId);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
