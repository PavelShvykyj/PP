using System.ComponentModel.DataAnnotations;

namespace PP.EF.models
{
    public class GoodsDTO
    {
        public ushort id { get; set; }
        public string name { get; set; }
        public ushort rest { get; set; }
        [Range(0,System.Double.MaxValue)]
        public decimal price { get; set; }
    }
}
