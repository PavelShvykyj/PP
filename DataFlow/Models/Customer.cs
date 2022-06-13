using System.ComponentModel.DataAnnotations;

namespace DataTier.Models
{
    public class Customer
    {
        public ushort Id { get; set; }
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
