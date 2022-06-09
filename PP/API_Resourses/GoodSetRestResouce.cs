using System.ComponentModel.DataAnnotations;

namespace PP.API_Resourses
{
    public class GoodSetRestResouce
    {
        [Required]
        [Range(0, System.Int16.MaxValue)]
        public ushort rest { get; set; }
        [Required]
        [Range(0, System.Int16.MaxValue)]
        public ushort id { get; set; }
    }
}
