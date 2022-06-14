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

        public async Task<CustomerDTO> CreateAsync(CustomerResource resource)
        {
            CustomerDTO CustomerDTO = null;
            Customer newCustomer = _unitOfWork.Customers.GetCustomerByEmail(resource.Email);
            if (newCustomer != null) 
            {
                CustomerDTO = _mapper.Map<Customer, CustomerDTO>(newCustomer);
                return CustomerDTO;
            }
            newCustomer = _mapper.Map<CustomerResource, Customer>(resource);
            _unitOfWork.Customers.Create(newCustomer);
            await _unitOfWork.SaveAsync();
            CustomerDTO = _mapper.Map<Customer, CustomerDTO>(newCustomer);

            return CustomerDTO;
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

        public IEnumerable<CustomerDTO> GetList(int skip, int take)
        {
            Customer[] customers = _unitOfWork.Customers.GetList( take,  skip)
               .Skip(skip)
               .Take(take)
               .ToArray();

            CustomerDTO[] customersDTOs = _mapper.Map<Customer[], CustomerDTO[]>(customers);
            return customersDTOs;
        }

        public async Task<CustomerDTO> UpdateAsync(int id, CustomerResource resource)
        {
            Customer customer = _unitOfWork.Customers.Get(id);
            if (customer is null)
            {
                return null;
            }
            _mapper.Map<CustomerResource, Customer>(resource, customer);
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<Customer, CustomerDTO>(customer);
        }
    }
}
