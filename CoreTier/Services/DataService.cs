using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interfaces;

namespace CoreTier.Services
{
    internal class DataService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public IGoodService GoodsServise { get; private set; }
        public ICustomerService CustomerServise { get; private set; }
        public IOrderService OrderServise { get; private set; }


        public DataService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
