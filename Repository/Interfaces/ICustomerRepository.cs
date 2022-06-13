using DataTier.Models;
namespace Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Customer GetCustomerByEmail(string email);
    }
}
