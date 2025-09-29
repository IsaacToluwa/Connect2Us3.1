using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using System.Collections.Generic;
using System.Linq;

namespace Connect2Us3._01.BLL
{
    public class BookBLL
    {
        private ApplicationDbContext _context;

        public BookBLL()
        {
            _context = new ApplicationDbContext();
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public void CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _context.Entry(book).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}