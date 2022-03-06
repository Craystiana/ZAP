using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using ZAP.API.Authorization;
using ZAP.BusinessLogic.DTO.Customer;
using ZAP.BusinessLogic.Services;
using ZAP.Common.Enums;

namespace ZAP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ZapAuthorize(UserRoleType.Admin)]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(CustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        [Route("All")]
        public IActionResult All()
        {
            return new JsonResult(_customerService.GetCustomerList());
        }

        [HttpGet]
        [Route("Detail")]
        public IActionResult Detail([FromQuery]int customerId)
        {
            try
            {
                var customerDetails = _customerService.GetCustomerDetails(customerId);

                return new JsonResult(customerDetails);
            }
            catch(Exception ex)
            {
                _logger.LogError("Unable to fetch customer details for customer with id " + customerId + ".\nError:\n" + ex);
                return new JsonResult(new CustomerDetailsModel());
            }
        }

        [HttpPost]
        [Route("Blacklist")]
        public IActionResult Blacklist([FromQuery]int customerId)
        {
            try
            {
                _customerService.Blacklist(customerId);
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to blacklist customer with id " + customerId + ".\nError:\n" + ex);
                return new JsonResult(false);
            }
        }
    }
}
