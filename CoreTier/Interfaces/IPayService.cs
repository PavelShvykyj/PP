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
        bool CanPay(int Id);
        void StartPay(int Id);
        void PayFinishSuccess(int Id);
        void PayFinishFail(int Id);
    }
}
