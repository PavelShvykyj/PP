using System.ComponentModel.DataAnnotations;

namespace PP.API_Resourses
{
    public class OrderGoodsSetResource
    {
        public int OrderId { get; set; }
        [Required]
        public ushort GoodId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Range(0.1, System.Double.MaxValue)]
        public decimal Quantity { get; set; }
    }
}
