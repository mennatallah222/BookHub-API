using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string ContactInformation { get; set; }
    }

}
