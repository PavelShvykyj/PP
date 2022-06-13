using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CoreTier.Interfaces;
using DTO.APIResourses;
using DTO.DTO;
using Repository.Interfaces;
using DataTier.Models;



namespace CoreTier.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDTO> Create(CustomerResource resource)
        {
            Customer newCustomer = _unitOfWork.Customers.GetCustomerByEmail(resource.Email);
            if (newCustomer != null) 
            {
                return _mapper.Map<Customer, CustomerDTO>(newCustomer);
            }
            newCustomer = _mapper.Map<CustomerResource, Customer>(resource);
            _unitOfWork.Customers.Create(newCustomer);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<Customer, CustomerDTO>(newCustomer);
        }

        public CustomerDTO Get(int id)
        {
            Customer Customer = _unitOfWork.Customers.Get(id);
            if (Customer == null) 
            {
                return null;
            }
            return _mapper.Map<Customer, CustomerDTO>(Customer);
        }

        public IEnumerable<CustomerDTO> GetList(int take, int skip)
        {
            throw new NotImplementedException();
        }

        public CustomerDTO Update(CustomerResource resource)
        {
            throw new NotImplementedException();
        }
    }
}
