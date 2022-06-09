using System.ComponentModel.DataAnnotations;

namespace PP.API_Resourses
{
    public class GoodSetPriceResouce
    {
        [Required]
        public decimal price { get; set; }

        [Required]
        [Range(0, System.Int16.MaxValue)]
        public ushort id { get; set; }
    }
}
