using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Connect2Us3._01.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}