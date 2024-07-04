using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public Cart Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
