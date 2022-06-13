namespace DataTier.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Summ { get; set; }
        public ushort CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string? Comment { get; set; }
        public ICollection<OrderRows> Goods { get; set; }
        public Order()
        {
            Goods = new List<OrderRows>();
        }
    }
}
