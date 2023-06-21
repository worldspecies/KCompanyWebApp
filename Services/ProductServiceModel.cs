using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace KCompanyWebApp.Services;

[DataContract]
public class ProductServiceList
{
    [DataMember]
    public List<ProductServiceModel> ProductServiceModels { get; set; }
}

[DataContract]
public class ProductServiceModel
{
    [DataMember]
    public string ProductNo { get; set; } = null!;
    [DataMember]
    public string? ProductDesc { get; set; }
    [DataMember]
    public string? ProductType { get; set; }
    [DataMember]
    public string? ProductBrand { get; set; }
    [DataMember]
    public string? UoM { get; set; }
    [DataMember]
    public decimal? Cogs { get; set; }
}
