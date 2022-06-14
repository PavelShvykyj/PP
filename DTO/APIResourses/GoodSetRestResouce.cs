using System.ComponentModel.DataAnnotations;

namespace DTO.APIResourses
{
    public class GoodSetRestResouce
    {
        [Required]
        [Range(0, System.Int16.MaxValue)]
        public ushort Rest { get; set; }
        [Required]
        [Range(0, System.Int16.MaxValue)]
        public ushort Id { get; set; }
    }
}
