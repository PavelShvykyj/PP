using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Models
{
    public class OrderPaymentProces
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string ExternalId { get; set; }
        public string ExternalName { get; set; }
        public DateTime Expired { get; set; }
    }
}
