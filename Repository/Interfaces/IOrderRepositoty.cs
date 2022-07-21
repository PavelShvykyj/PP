using DataTier.Models;
using DTO.DTO;

namespace Repository.Interfaces
{
    public interface IOrderRepositoty : IRepository<Order>
    {
        OrderPaymentProces? GetPaymentProces(int Orderid);
        OrderPaymentDitail? GetPaymentDitail(int Orderid);
        Order GetOrderWithProperties(int id);
        List<OrdersListDTO> GetOrdersShortList(int take, int skip);
        void AddGoods(OrderRows[] goods);
    }
}
