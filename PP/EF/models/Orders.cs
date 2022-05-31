namespace PP.EF.models
{
    public class Orders
    {
        public DateTime id { get; set; }
        public decimal summ { get; set; }
        public ushort customerid { get; set; }
        public Customers customer { get; set; }
        public string? comment { get; set; }
        public ICollection<OrderRows> rows { get; set; }

    }
}
