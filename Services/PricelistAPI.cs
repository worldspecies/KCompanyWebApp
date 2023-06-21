using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace KCompanyWebApp.Services
{
    [ServiceContract]
    public interface PricelistAPI
    {
        [OperationContract]
        public PricelistServiceList GetPricelistByStoreAreaNo(string storeNo, string areaNo);
    }
}
