using BusinessObject.Models;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class PublisherDAO
    {
        private readonly eBookStoreContext _context;

        public PublisherDAO(eBookStoreContext context)
        {
            _context = context;
        }

        public List<Publisher> GetAll()
        {
            return _context.Publisher.ToList();
        }

        public Publisher GetById(int pubId)
        {
            return _context.Publisher.Find(pubId);
        }

        public void Add(Publisher publisher)
        {
            _context.Publisher.Add(publisher);
            _context.SaveChanges();
        }

        public void Update(Publisher publisher)
        {
            _context.Publisher.Update(publisher);
            _context.SaveChanges();
        }

        public void Delete(int pubId)
        {
            var publisher = _context.Publisher.Find(pubId);
            if (publisher != null)
            {
                _context.Publisher.Remove(publisher);
                _context.SaveChanges();
            }
        }
    }
}
