using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Interfaces
{
    public interface IDataService
    {
        public IGoodService GoodService { get;  }
        public ICustomerService CustomerService { get;  }
        public IOrderService OrderService { get;  }

    }
}
