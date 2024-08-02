using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
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
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required, Range(1, 5)]
        public int Rating { get; set; }
        public string? Comment { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }



        public virtual ICollection<User> ReviewedByUsers { get; set; }

    }
}
