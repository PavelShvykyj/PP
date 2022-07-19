using System.ComponentModel.DataAnnotations;

namespace DataTier.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
