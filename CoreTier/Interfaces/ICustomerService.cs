using DataTier.Models;
using DTO.APIResourses;
using DTO.DTO;

namespace CoreTier.Interfaces
{
    public interface ICustomerService : IUnitService<CustomerResource, CustomerDTO>
    {
        bool IsEmailExists(string email);
    }
}
