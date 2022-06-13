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
    internal class CustomerRepository : Repository<Customer> , ICustomerRepository
    {
        private readonly ApplicationContext _context;

        public CustomerRepository(ApplicationContext context)
            : base(context)
        {
            _context = context;
        }

        public Customer GetCustomerByEmail(string email) {
            Customer[] Customers = _context
                .Customers
                .Where(c => c.Email == email)
                .ToArray();

            if (Customers.Count() != 0)
            {
                return Customers[0];
            }
            return null;
        }

    }
}
