using Alachisoft.NCache.Licensing.DOM;
using KCompanyWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;
using System.Net;

namespace KCompanyWebApp.Services
{
    public class PricelistService : PricelistAPI
    {
        public PricelistServiceList GetPricelistByStoreAreaNo(string storeNo, string areaNo)
        {
            var dbContext = new MyDBContext();
            var PriceConfigs = (from pric in dbContext.MsPricingConfigs
                            join prod in dbContext.MsProductSpareparts on pric.ProductNo equals prod.ProductNo
                            where pric.AreaNo == areaNo && pric.StoreNo == storeNo
                            orderby pric.AreaNo, pric.StoreNo, prod.ProductNo
                            select new
                            {
                                StoreNo = pric.StoreNo,
                                AreaNo = pric.AreaNo,
                                CustomerType = pric.CustomerType,
                                ValidFrom = pric.ValidFrom,
                                ValidTo = pric.ValidTo,
                                ProductNo = prod.ProductNo,
                                ProductDesc = prod.ProductDesc,
                                ProductBrand = prod.ProductBrand,
                                UoM = prod.UoM,
                                Amount = pric.Amount
                            }).ToList();

            List<PricelistServiceModel> list_price = new List<PricelistServiceModel>();
            foreach (var row in PriceConfigs)
            {
                var sm_price = new PricelistServiceModel();
                sm_price.StoreNo = row.StoreNo;
                sm_price.AreaNo = row.AreaNo;
                sm_price.CustomerType = row.CustomerType;
                sm_price.ValidFrom = row.ValidFrom;
                sm_price.ValidTo = row.ValidTo;
                sm_price.ProductNo = row.ProductNo;
                sm_price.ProductDesc = row.ProductDesc;
                sm_price.ProductBrand = row.ProductBrand;
                sm_price.UoM = row.UoM;
                sm_price.Amount = row.Amount;
                list_price.Add(sm_price);
            }

            var sl_pricelist = new PricelistServiceList();
            sl_pricelist.PricelistServiceModels = list_price;
            return sl_pricelist;
        }
    }
}
