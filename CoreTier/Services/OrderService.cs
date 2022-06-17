using AutoMapper;
using CoreTier.Interfaces;
using DataTier.Models;
using DTO.APIResourses;
using DTO.DTO;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<OrderDTO> CreateAsync(OrderSetResource orderdata)
        {
            
            /// ----------- Importent group order goods by good id , summ quantaty
            /// good id + price -- must be unique
            /// else mapping rows cant work
            
            Order order = new Order();
            _mapper.Map<OrderSetResource, Order>(orderdata, order);
            _ = order.Goods.Select(r => { r.Order = order; return r; }).ToList();
            _unitOfWork.Orders.Create(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<Order, OrderDTO>(order);



            //order.Goods.Clear();
            //_unitOfWork.Orders.Create(order);
            //await _unitOfWork.SaveAsync();
            //_ = orderdata.Goods.Select(r => { r.OrderId = order.Id; return r; }).ToList();
            //OrderRows[] orderGoods = _mapper.Map<OrderGoodsSetResource[], OrderRows[]>(orderdata.Goods.ToArray());
            //_unitOfWork.Orders.AddGoods(orderGoods);
            //await _unitOfWork.SaveAsync();
            //order = _unitOfWork.Orders.GetOrderWithProperties(order.Id);
            //return _mapper.Map<Order, OrderDTO>(order);
        }
        public OrderDTO Get(int id)
        {
            Order order = _unitOfWork.Orders.Get(id);
            if (order is null)
            {
                return null;
            }
            return _mapper.Map<Order, OrderDTO>(order);
        }
        public List<OrdersListDTO> GetList(int take, int skip)
        {
            List<OrdersListDTO> orders = _unitOfWork.Orders.GetOrdersShortList(take, skip);
            return orders;
        }
        public async Task<OrderDTO> UpdateAsync(int id, OrderSetResource orderdata)
        {
            Order order = _unitOfWork.Orders.GetOrderWithProperties(id);
            if (order is null)
            {
                return null;
            }
            _mapper.Map<OrderSetResource, Order>(orderdata, order);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveAsync();
            
            return _mapper.Map<Order, OrderDTO>(order);
        }

        IEnumerable<OrderDTO> IUnitService<OrderSetResource, OrderDTO>.GetList(int take, int skip)
        {
            throw new NotImplementedException();
        }
    }
}
