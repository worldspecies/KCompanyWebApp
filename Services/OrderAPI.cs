using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace KCompanyWebApp.Services
{
    [ServiceContract]
    public interface OrderAPI
    {
        [OperationContract]
        public OrderServiceList GetOrderByNo(string orderNo);
        [OperationContract]
        public OrderServiceModel AddNewOrder(CreateOrderServiceModel order);

        [OperationContract]
        public OrderServiceModel EditOrder(OrderServiceModel orderNo);

        [OperationContract]
        public OrderServiceModel VoidOrder(string orderNo, string salesmanNo);

        /* details */
        [OperationContract]
        public DetailServiceList GetOrderDetails(string orderNo);
        [OperationContract]
        public DetailServiceModel AddNewDetails(DetailServiceModel det);
        [OperationContract]
        public string DeleteOrderDetails(int detID);
    }
}
