using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class BookGenre
    {
        [Key]
        public int BookGenreId { get; set; }


        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public virtual Category Genre { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Product Book { get; set; }
    }
}
