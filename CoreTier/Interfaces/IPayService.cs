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
        Task<bool> CanPayAsync(int Id);
        Task<string> StartPayAsync(int Id, string CancelUrl, string SuccessUrl);
        Task PayFinishSuccessAsync(int Id);
        Task PayFinishFailAsync(int Id);
    }
}
