using DataTier.Models;
using DTO.APIResourses;
using DTO.DTO;

namespace Repository.Interfaces
{
    public interface ICustomerService : IUnitService<CustomerResource, CustomerDTO>
    {
    }
}
