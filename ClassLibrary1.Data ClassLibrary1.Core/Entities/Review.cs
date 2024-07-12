using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string? Content { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Required, Range(1, 5)]
        public int Rating { get; set; } // e.g., 1 to 5 stars
        public string? Comment { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }

    }
}
