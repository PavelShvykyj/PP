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
    internal class OrderPaymentDetailRepository : Repository<OrderPaymentDitail>, IOrderPaymentDitailRepository
    {
        private ApplicationContext _db
        {
            get { return _context as ApplicationContext; }
        }
        public OrderPaymentDetailRepository(ApplicationContext context)
            : base(context)
        {
        }
        public void Remove(int id)
        {
            var detail = _db.OrderPaymentDetails.SingleOrDefault(p => p.Id == id);
            if (detail is not null)
            {
                _db.OrderPaymentDetails.Remove(detail);
            }
        }
    }
}
