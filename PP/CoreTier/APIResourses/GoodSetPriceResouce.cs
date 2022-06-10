using System.ComponentModel.DataAnnotations;

namespace PP.CoreTier.APIResourses
{
    public class GoodSetPriceResouce
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(0, System.Int16.MaxValue)]
        public ushort Id { get; set; }
    }
}
