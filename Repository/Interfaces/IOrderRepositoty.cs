using DataTier.Models;

namespace Repository.Interfaces
{
    public interface IOrderRepositoty : IRepository<Order>
    {
        Order GetOrderWithProperties(int id);
        IEnumerable<Object> GetOrdersShortList(int take, int skip);
        void AddGoods(OrderRows[] goods);
    }
}
