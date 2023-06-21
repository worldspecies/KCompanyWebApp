using KCompanyWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KCompanyWebApp.Services
{
    public class CustomerService : CustomerAPI
    {
        public CustomerServiceList GetCustomerByName(string name)
        {
            var dbContext = new MyDBContext();
            var Customers = (from cust in dbContext.MsCustomers
                            where EF.Functions.Like(cust.CustomerName, "%" + name + "%")
                            orderby cust.CustomerName
                            select cust).ToList();

            List<CustomerServiceModel> list_customer = new List<CustomerServiceModel>();
            foreach (var row in Customers)
            {
                var sm_customer = new CustomerServiceModel();
                sm_customer.CustomerNo = row.CustomerNo;
                sm_customer.CustomerType = row.CustomerType;
                sm_customer.CustomerName = row.CustomerName;
                sm_customer.Address = row.Address;
                sm_customer.Phone = row.Phone;
                list_customer.Add(sm_customer);
            }

            var sl_customer = new CustomerServiceList();
            sl_customer.CustomerServiceModels = list_customer;
            return sl_customer;
        }
    }
}
