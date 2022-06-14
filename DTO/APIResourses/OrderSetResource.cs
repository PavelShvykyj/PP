using System.ComponentModel.DataAnnotations;

namespace DTO.APIResourses
{
    public class OrderSetResource
    {
        [Required]
        public ushort CustomerId { get; set; }
        public string? Comment { get; set; }
        [Required]
        public ICollection<OrderGoodsSetResource> Goods { get; set; }
    }
}
