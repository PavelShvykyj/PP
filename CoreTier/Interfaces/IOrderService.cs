using DataTier.Models;
using DTO.APIResourses;
using DTO.DTO;

namespace CoreTier.Interfaces
{
    public interface IOrderService : IUnitService<OrderSetResource, OrderDTO>
    {
        List<OrdersListDTO> GetList(int take, int skip);
    }
}
