using DataTier;
using DataTier.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal class OrderRepository : Repository<Order>, IOrderRepositoty
    {
        private ApplicationContext _db
        {
            get { return _context as ApplicationContext; }
        }
        public OrderRepository(ApplicationContext context)
            : base(context)
        {

        }
        public List<OrdersListDTO> GetOrdersShortList(int take, int skip) 
        {
            return _db.Orders.Include(o => o.Customer)
                            .OrderBy(o => o.CustomerId)
                            .Skip(skip)
                            .Take(take)
                            .Select(o => new OrdersListDTO { Id = o.Id, Summ = o.Summ, CustomerId = o.CustomerId , CustomerName = o.Customer.Name })
                            .ToList();
        }

        public Order GetOrderWithProperties(int id) 
        {

            return _db.Orders.Include(c => c.Customer)
                  .Include(c => c.Goods)
                  .ThenInclude(r => r.Good)
                  .Single(o => o.Id == id);
        }

        public void AddGoods(OrderRows[] orderGoods) {
            _db.OrderRows.AddRange(orderGoods);
        }
    }
}
