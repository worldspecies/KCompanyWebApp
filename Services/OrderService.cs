using Alachisoft.NCache.EntityFrameworkCore;
using KCompanyWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace KCompanyWebApp.Services
{
    public class OrderService : OrderAPI
    {
        public OrderServiceList GetOrderByNo(string orderNo)
        {
            var dbContext = new MyDBContext();
            var Orders = (from ord in dbContext.TrOrderHdrs
                         where ord.OrderNo == orderNo
                         orderby ord.OrderNo, ord.OrderDate
                         select new
                         {
                             OrderNo = ord.OrderNo,
                             OrderDate = ord.OrderDate,
                             StoreNo = ord.StoreNo,
                             AreaNo = ord.AreaNo,
                             SalesmanNo = ord.SalesmanNo,
                             CustomerNo = ord.CustomerNo,
                             GrandTotal = ord.GrandTotal,
                             Description = ord.Description
                         }).ToList();

            List<OrderServiceModel> list_order = new List<OrderServiceModel>();
            foreach (var row in Orders)
            {
                var sm_order = new OrderServiceModel();
                sm_order.OrderNo = row.OrderNo;
                sm_order.OrderDate = row.OrderDate;
                sm_order.StoreNo = row.StoreNo;
                sm_order.AreaNo = row.AreaNo;
                sm_order.SalesmanNo = row.SalesmanNo;
                sm_order.CustomerNo = row.CustomerNo;
                sm_order.GrandTotal = row.GrandTotal;
                sm_order.Description = row.Description;
                list_order.Add(sm_order);
            }

            var sl_orders = new OrderServiceList();
            sl_orders.OrderServiceModels = list_order;
            return sl_orders;
        }
        public OrderServiceModel AddNewOrder(CreateOrderServiceModel order)
        {
            var dbContext = new MyDBContext();
            var sm_order = new OrderServiceModel();
            var newOrder = new TrOrderHdr();

            if (order != null)
            {
                //get last order numb
                var prevOrder = dbContext.TrOrderHdrs.OrderByDescending(f => f.OrderNo).FirstOrDefault();
                var newOrderNumb = "00001";
                if (prevOrder != null)
                {
                    Regex regexObj = new Regex(@"[^\d]");
                    newOrderNumb = regexObj.Replace(prevOrder.OrderNo, "");
                }
                int parsedOrderNumb = Convert.ToInt32(newOrderNumb);
                parsedOrderNumb += 1;
                newOrderNumb = "" + parsedOrderNumb;

                //filling New Order
                newOrder.OrderNo = "ORD" + newOrderNumb.PadLeft(5, '0');
                newOrder.OrderDate = DateTime.Now;
                newOrder.GrandTotal = 0;
                newOrder.CrtUsrId = order.SalesmanNo;
                newOrder.TsCrt = DateTime.Now;
                newOrder.ModUsrId = order.SalesmanNo;
                newOrder.TsMod = DateTime.Now;
                newOrder.ActiveFlag = "Y";

                //fill data from salesman
                newOrder.StoreNo = order.StoreNo;
                newOrder.AreaNo = order.AreaNo;
                newOrder.SalesmanNo = order.SalesmanNo;
                newOrder.CustomerNo = order.CustomerNo;
                newOrder.Description = order.Description;

                dbContext.TrOrderHdrs.Add(newOrder);
                dbContext.SaveChanges();

                //returns model
                sm_order.OrderNo = newOrder.OrderNo;
                sm_order.OrderDate = newOrder.OrderDate;
                sm_order.StoreNo = newOrder.StoreNo;
                sm_order.AreaNo = newOrder.AreaNo;
                sm_order.SalesmanNo = newOrder.SalesmanNo;
                sm_order.CustomerNo = newOrder.CustomerNo;
                sm_order.Description = newOrder.Description;
                sm_order.GrandTotal = newOrder.GrandTotal;
            }

            return sm_order;
        }
        public OrderServiceModel EditOrder(OrderServiceModel order)
        {
            var dbContext = new MyDBContext();
            var revisedOrder = new TrOrderHdr();
            var sm_order = new OrderServiceModel();

            if (order != null)
            {
                sm_order = order; //copy

                revisedOrder = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y" && f.OrderNo == sm_order.OrderNo).FirstOrDefault();
                revisedOrder.StoreNo = sm_order.StoreNo;
                revisedOrder.AreaNo = sm_order.AreaNo;
                revisedOrder.SalesmanNo = sm_order.SalesmanNo;
                revisedOrder.CustomerNo = sm_order.CustomerNo;
                revisedOrder.Description = sm_order.Description;
                revisedOrder.GrandTotal = sm_order.GrandTotal;
                revisedOrder.ModUsrId = sm_order.SalesmanNo;
                revisedOrder.TsMod = DateTime.Now;

                dbContext.TrOrderHdrs.Update(revisedOrder);
                dbContext.SaveChanges();
            }

            return sm_order;
        }
        
        public OrderServiceModel VoidOrder(string orderNo, string salesmanNo)
        {
            var dbContext = new MyDBContext();
            var sm_order = new OrderServiceModel();

            var Order = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y" && f.OrderNo == orderNo).FirstOrDefault();
            if (Order != null)
            {
                //copy
                sm_order.OrderNo = Order.OrderNo;
                sm_order.OrderDate = Order.OrderDate;
                sm_order.StoreNo = Order.StoreNo;
                sm_order.AreaNo = Order.AreaNo;
                sm_order.SalesmanNo = Order.SalesmanNo;
                sm_order.CustomerNo = Order.CustomerNo;
                sm_order.GrandTotal = Order.GrandTotal;
                sm_order.Description = Order.Description;

                //making changes
                Order.ModUsrId = salesmanNo;
                Order.TsMod = DateTime.Now;
                Order.ActiveFlag = "N";
                dbContext.TrOrderHdrs.Update(Order);
                dbContext.SaveChanges();
            }

            return sm_order;
        }

        /* details */
        public DetailServiceList GetOrderDetails(string orderNo)
        {
            var dbContext = new MyDBContext();
            var Details = (from det in dbContext.TrOrderDtls
                          where det.OrderNo == orderNo
                          orderby det.OrderNo, det.ProductNo
                          select det).ToList();

            List<DetailServiceModel> list_detail = new List<DetailServiceModel>();
            foreach (var row in Details)
            {
                var sm_detail = new DetailServiceModel();
                sm_detail.OrderNo = row.OrderNo;
                sm_detail.ProductNo = row.ProductNo;
                sm_detail.Quantity = row.Quantity;
                sm_detail.Price = row.Price;
                sm_detail.Total = row.Total;
                sm_detail.Description = row.Description;
                list_detail.Add(sm_detail);
            }

            var sl_details = new DetailServiceList();
            sl_details.DetailServiceModels = list_detail;
            return sl_details;
        }

        public DetailServiceModel AddNewDetails(DetailServiceModel det)
        {
            var dbContext = new MyDBContext();
            var sm_detail = new DetailServiceModel();
            var newDet = new TrOrderDtl();

            if (det != null)
            {
                //filling New Order
                newDet.OrderNo = det.OrderNo;
                newDet.ProductNo = det.ProductNo;
                newDet.Quantity = det.Quantity;
                newDet.Price = det.Price;
                newDet.Total = det.Total;
                newDet.Description = det.Description;
                newDet.ActiveFlag = "Y";
                dbContext.TrOrderDtls.Add(newDet);
                dbContext.SaveChanges();

                //update order
                var ExistingOrder = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y" && f.OrderNo == det.OrderNo).FirstOrDefault();
                if (ExistingOrder != null)
                {
                    ExistingOrder.GrandTotal = ExistingOrder.GrandTotal + det.Total;
                    dbContext.TrOrderHdrs.Update(ExistingOrder);
                    dbContext.SaveChanges();
                }

                //returns model
                sm_detail.OrderNo = newDet.OrderNo;
                sm_detail.ProductNo = newDet.ProductNo;
                sm_detail.Quantity = newDet.Quantity;
                sm_detail.Price = newDet.Price;
                sm_detail.Total = newDet.Total;
                sm_detail.Description = newDet.Description;
            }

            return sm_detail;
        }

        public string DeleteOrderDetails(int detID)
        {
            var dbContext = new MyDBContext();
            var Message = "Delete Detail Failed";

            if (detID != 0)
            {
                var OrderDtl = dbContext.TrOrderDtls.Where(f => f.ActiveFlag == "Y" && f.OrderDtlID == detID).FirstOrDefault();
                var OrderNo = "";

                OrderNo = OrderDtl.OrderNo;
                var ExistingOrder = dbContext.TrOrderHdrs.Where(f => f.ActiveFlag == "Y" && f.OrderNo == OrderNo).FirstOrDefault();
                if (ExistingOrder != null)
                {
                    ExistingOrder.GrandTotal = ExistingOrder.GrandTotal - OrderDtl.Total;
                    dbContext.TrOrderHdrs.Update(ExistingOrder);
                    dbContext.SaveChanges();
                }

                dbContext.TrOrderDtls.Remove(OrderDtl);
                dbContext.SaveChanges();

                Message = "Delete Detail Successful";
            }

            return Message;
        }
    }
}
