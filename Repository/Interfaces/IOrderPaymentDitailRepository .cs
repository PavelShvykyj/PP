using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IOrderPaymentDitailRepository : IRepository<OrderPaymentDitail>
    {
        void Remove(int id);
    }
}
