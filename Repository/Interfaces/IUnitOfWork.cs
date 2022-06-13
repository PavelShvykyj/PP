using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    internal interface IUnitOfWork : IDisposable
    {
        IGoodRepository Goods { get;  }
        ICustomerRepository Customers { get; }
        IOrderRepositoty Orders { get; }
        Task<int> SaveAsync();
    }
}
