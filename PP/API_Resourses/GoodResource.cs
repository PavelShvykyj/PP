using System.ComponentModel.DataAnnotations;

namespace PP.API_Resourses
{
    public class GoodResource
    {
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        [Range(0, System.Int16.MaxValue)]
        
        public ushort rest { get; set; }
        
        [Range(0, System.Double.MaxValue)]
        public decimal price { get; set; }
    }
}
