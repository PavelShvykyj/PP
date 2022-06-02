using System.ComponentModel.DataAnnotations;

namespace PP.EF.models
{
    public class OrderRowsDTO
    {
        public int id { get; set; }
        public DateTime orderid { get; set; }
        public OrdersDTO order { get; set; }
        public ushort goodid { get; set; }
        public GoodsDTO good { get; set; }
        public decimal price { get; set; }
        public decimal summ { get; set; }
        public decimal quantity { get; set; }
    }
}
