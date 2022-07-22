using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Models
{
    public class OrderPaymentDitail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Amaunt { get; set; }
        public string ExternalID { get; set; }
        public string CartType { get; set; }
        public string CartNubmer { get; set; }
    }
}
