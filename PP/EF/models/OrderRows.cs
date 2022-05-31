using System.ComponentModel.DataAnnotations;

namespace PP.EF.models
{
    public class OrderRows
    {
        public int id { get; set; }
        public DateTime orderid { get; set; }
        public Orders order { get; set; }
        public ushort goodid { get; set; }
        public Goods good { get; set; }
        [Range(0, System.Double.MaxValue)]
        public decimal price { get; set; }
        public decimal summ { get; set; }
        public ushort quantity { get; set; }
    }
}
