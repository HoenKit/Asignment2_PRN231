using BusinessObject.Models;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class AuthorDAO
    {
        private readonly eBookStoreContext _context;

        public AuthorDAO(eBookStoreContext context)
        {
            _context = context;
        }

        public List<Author> GetAll()
        {
            return _context.Author.ToList();
        }

        public Author GetById(int authorId)
        {
            return _context.Author.Find(authorId);
        }

        public void Add(Author author)
        {
            _context.Author.Add(author);
            _context.SaveChanges();
        }

        public void Update(Author author)
        {
            _context.Author.Update(author);
            _context.SaveChanges();
        }

        public void Delete(int authorId)
        {
            var author = _context.Author.Find(authorId);
            if (author != null)
            {
                _context.Author.Remove(author);
                _context.SaveChanges();
            }
        }
    }
}
