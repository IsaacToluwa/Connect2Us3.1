using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connect2Us3._01.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(20)]
        public string ISBN { get; set; }

        [StringLength(200)]
        public string Publisher { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PublishedDate { get; set; }

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal")]
        public decimal? RentalPrice { get; set; }

        public int StockLevel { get; set; }

        public bool IsRentable { get; set; }
    }
}