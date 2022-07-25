
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO
{
    public class OrderPaymentDitailDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }
        public int Amaunt { get; set; }
        public string ExternalID { get; set; }
        public string CartType { get; set; }
        public string CartNubmer { get; set; }
    }
}
