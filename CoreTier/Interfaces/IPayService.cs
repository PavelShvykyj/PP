using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Interfaces
{
    public interface IPayService
    {
        /// <summary>
        ///  Id - id of order to be payed
        /// </summary>
        bool CanPay(string Id);
        void StartPay(string Id);
        void PayFinishSuccess(string Id);
        void PayFinishFail(string Id);
    }
}
