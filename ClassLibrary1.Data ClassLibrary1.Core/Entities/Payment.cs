using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        [Required, MaxLength(20)]
        public string Status { get; set; } = "Pending"; //pending, paid
        public string PaymentType { get; set; }
        public DateTime? PaymentDate { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Required, MaxLength(50)]
        public string PaymentMethod { get; set; }

    }
}
