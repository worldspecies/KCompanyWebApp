using KCompanyWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KCompanyWebApp.Services
{
    public class ProductService : ProductAPI
    {
        public ProductServiceList GetProductByDescription(string desc)
        {
            var dbContext = new MyDBContext();
            var Products = (from prod in dbContext.MsProductSpareparts
                            where EF.Functions.Like(prod.ProductDesc, "%" + desc + "%")
                            orderby prod.ProductNo
                            select prod).ToList();

            List<ProductServiceModel> list_product = new List<ProductServiceModel>();
            foreach (var row in Products)
            {
                var sm_product = new ProductServiceModel();
                sm_product.ProductNo = row.ProductNo;
                sm_product.ProductDesc = row.ProductDesc;
                sm_product.ProductType = row.ProductType;
                sm_product.ProductBrand = row.ProductBrand;
                sm_product.UoM = row.UoM;
                sm_product.Cogs = row.Cogs;
                list_product.Add(sm_product);
            }

            var sl_product = new ProductServiceList();
            sl_product.ProductServiceModels = list_product;
            return sl_product;
        }
    }
}
