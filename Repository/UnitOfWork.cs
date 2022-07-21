using DataTier;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGoodRepository Goods { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IOrderRepositoty Orders { get; private set; }
        public IOrderPaymentDitailRepository OrderPaymentDitails { get; private set; }
        public IOrderPaymentProcesRepository OrderPaymentProcess { get; private set; }


        private readonly ApplicationContext _context; 
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Goods = new GoodRepository(_context);
            Customers = new CustomerRepository(_context);
            Orders = new OrderRepository(_context);
            OrderPaymentDitails = new OrderPaymentDetailRepository(_context);
            OrderPaymentProcess = new OrderPaymentProcesRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int>  SaveAsync()
        {
            return  await _context.SaveChangesAsync();
        }
    }
}
