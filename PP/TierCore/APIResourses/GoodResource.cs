using System.ComponentModel.DataAnnotations;

namespace PP.APIResourses
{
    public class GoodResource
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Range(0, System.Int16.MaxValue)]
        
        public ushort Rest { get; set; }
        
        [Range(0, System.Double.MaxValue)]
        public decimal Price { get; set; }
    }
}
