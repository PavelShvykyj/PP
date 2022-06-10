using System.ComponentModel.DataAnnotations;

namespace PP.CoreTier.DTO
{
    public class OrderRowsDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public GoodDTO Good { get; set; }
        public ushort GoodId { get; set; }
        public decimal Price { get; set; }
        public decimal Summ { get; set; }
        public decimal Quantity { get; set; }
    }
}
