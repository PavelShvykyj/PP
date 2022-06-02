namespace PP.EF.models
{
    public class OrdersDTO
    {
        public DateTime id { get; set; }
        public decimal summ { get; set; }
        public ushort customerid { get; set; }
        public CustomersDTO customer { get; set; }
        public string? comment { get; set; }
        public ICollection<OrderRowsDTO> rows { get; set; }

    }
}
