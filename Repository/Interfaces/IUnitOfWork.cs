using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGoodRepository Goods { get;  }
        ICustomerRepository Customers { get; }
        IOrderRepositoty Orders { get; }
        IOrderPaymentDitailRepository OrderPaymentDitails { get; }
        IOrderPaymentProcesRepository OrderPaymentProcess { get; }
        Task<int> SaveAsync();
    }
}
