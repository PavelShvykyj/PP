namespace DTO.DTO
{
    public class OrdersListDTO
    {
        
        public DateTime Id { get; set; }
        public decimal Summ { get; set; }
        public ushort CustomerId { get; set; }
        public string CustomerName { get; set; }

    }
}
