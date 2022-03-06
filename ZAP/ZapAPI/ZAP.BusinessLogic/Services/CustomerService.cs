using System.Collections.Generic;
using System.Linq;
using ZAP.BusinessLogic.DTO.Customer;
using ZAP.Common.Enums;
using ZAP.DataAccess.Interfaces;

namespace ZAP.BusinessLogic.Services
{
    public class CustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CustomerModel> GetCustomerList()
        {
            var customerList = _unitOfWork.UserRepository.Find(c => c.UserRoleId == (int)UserRoleType.Customer).OrderBy(c => c.LastName);
            foreach(var customer in customerList)
            {
                yield return new CustomerModel
                {
                    CustomerId = customer.UserId,
                    Email = customer.EmailAddress,
                    LastName = customer.LastName,
                    FirstName = customer.FirstName,
                    IsBlacklisted = customer.IsBlacklisted
                };
            }
        }

        public CustomerDetailsModel GetCustomerDetails(int customerId)
        {
            var customer = _unitOfWork.UserRepository.Get(customerId);

            return new CustomerDetailsModel
            {
                CustomerId = customer.UserId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.EmailAddress,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber,
                IsBlacklisted = customer.IsBlacklisted
            };
        }

        public void Blacklist(int customerId)
        {
            var customer = _unitOfWork.UserRepository.Get(customerId);

            customer.IsBlacklisted = !customer.IsBlacklisted;

            _unitOfWork.SaveChanges();
        }
    }
}
