using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace KCompanyWebApp.Services
{
    [ServiceContract]
    public interface CustomerAPI
    {
        [OperationContract]
        public CustomerServiceList GetCustomerByName(string name);
    }
}
