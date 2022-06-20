using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTier.Interfaces;
using Repository.Interfaces;

namespace CoreTier.Services
{
    public class DataService : IDataService, IDisposable
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public IGoodService GoodService { get; private set; }
        public ICustomerService CustomerService { get; private set; }
        public IOrderService OrderService { get; private set; }

        public DataService(
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            GoodService = new GoodService(_mapper, _unitOfWork);
            CustomerService = new CustomerService(_mapper, _unitOfWork);
            OrderService = new OrderService(_mapper, _unitOfWork);
        }

        public void Dispose()
        {
           _unitOfWork.Dispose();
        }
    }
}
