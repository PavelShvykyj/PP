namespace PP.CoreTier.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public decimal Summ { get; set; }
        public ushort CustomerId { get; set; }
        public CustomerDTO Customer { get; set; }
        public string? Comment { get; set; }
        public ICollection<OrderRowsDTO> Goods { get; set; }

    }
}
