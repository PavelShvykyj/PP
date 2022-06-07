namespace PP.DTO
{
    public class OrdersListDTO
    {
        public DateTime id { get; set; }
        public decimal summ { get; set; }
        public ushort customerid { get; set; }
        public string customername { get; set; }

    }
}
