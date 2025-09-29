using Connect2Us3._01.DAL;
using Connect2Us3._01.Models;
using System.Collections.Generic;
using System.Linq;

namespace Connect2Us3._01.BLL
{
    public class ReviewBLL
    {
        private ApplicationDbContext _context;

        public ReviewBLL()
        {
            _context = new ApplicationDbContext();
        }

        public List<Review> GetAllReviews()
        {
            return _context.Reviews.ToList();
        }

        public Review GetReviewById(int id)
        {
            return _context.Reviews.Find(id);
        }

        public void CreateReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public void UpdateReview(Review review)
        {
            _context.Entry(review).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}