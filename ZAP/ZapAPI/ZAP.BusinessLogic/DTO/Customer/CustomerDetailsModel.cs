﻿namespace ZAP.BusinessLogic.DTO.Customer
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsBlacklisted { get; set; }
    }
}
