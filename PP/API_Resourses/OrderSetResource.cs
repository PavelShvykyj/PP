using System.ComponentModel.DataAnnotations;

namespace PP.API_Resourses
{
    public class OrderSetResource
    {
        [Required]
        public ushort customerid { get; set; }
        public string? comment { get; set; }
        [Required]
        public ICollection<OrderRowsSetResource> rows { get; set; }
    }
}
