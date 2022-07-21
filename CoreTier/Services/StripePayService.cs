using AutoMapper;
using CoreTier.Interfaces;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Services
{
    
    public class StripePayService : IPayService
    {
        public StripePayService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {

        }

        public bool CanPay(string Id)
        {
            throw new NotImplementedException();
        }

        public void PayFinishFail(string Id)
        {
            throw new NotImplementedException();
        }

        public void PayFinishSuccess(string Id)
        {
            throw new NotImplementedException();
        }

        public void StartPay(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
