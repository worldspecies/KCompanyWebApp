using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace KCompanyWebApp.Services
{
    [ServiceContract]
    public interface LoginAPI
    {
        [OperationContract]
        public LoginServiceModel Authenticate(string uid, string passwd);
    }
}
