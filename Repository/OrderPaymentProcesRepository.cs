using DataTier;
using DataTier.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal class OrderPaymentProcesRepository : Repository<OrderPaymentProces>, IOrderPaymentProcesRepository
    {
        private ApplicationContext _db
        {
            get { return _context as ApplicationContext; }
        }
        public OrderPaymentProcesRepository(ApplicationContext context)
            : base(context)
        {
        }
        public void Remove(int id)
        {
            var process = _db.OrderPaymentProceses.SingleOrDefault(p => p.Id == id);
            if (process is not null)
            {
                _db.OrderPaymentProceses.Remove(process);
            }
        }

        public void Remove(OrderPaymentProces process) {
            _db.OrderPaymentProceses.Remove(process);
        }
    }
}
