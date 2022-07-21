using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IOrderPaymentProcesRepository : IRepository<OrderPaymentProces>
    {
        void Remove(int id);
    }
}
