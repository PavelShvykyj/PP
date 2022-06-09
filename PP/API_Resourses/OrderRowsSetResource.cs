using System.ComponentModel.DataAnnotations;

namespace PP.API_Resourses
{
    public class OrderRowsSetResource
    {
        public int orderid { get; set; }
        [Required]
        public ushort goodid { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        [Range(0.1, System.Double.MaxValue)]
        public decimal quantity { get; set; }
    }
}
