using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CustomerAddresses
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(100)]
        public string AdressLine1 { get; set; }
        [MaxLength(100)]
        public string AdressLine2 { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string State { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
    }
}