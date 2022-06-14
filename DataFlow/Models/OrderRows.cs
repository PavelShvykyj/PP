using System.ComponentModel.DataAnnotations;

namespace DataTier.Models
{
    public class OrderRows
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public ushort GoodId { get; set; }
        public Good Good { get; set; }
        public decimal Price { get; set; }
        public decimal Summ { get; set; }
        public decimal Quantity { get; set; }
    }
}
