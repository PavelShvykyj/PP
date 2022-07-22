using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO
{
    public class OrderPaymentProcesDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }
        public string ExternalId { get; set; }
        public string ExternalName { get; set; }
        public DateTime Expired { get; set; }
    }
}
