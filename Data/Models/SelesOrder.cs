using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class SelesOrder
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Customer Customer { get; set; }
        public List<SelesOrderItem> SelesOrderItems { get; set; }
        public bool IsPaid { get; set; }
    }
}
