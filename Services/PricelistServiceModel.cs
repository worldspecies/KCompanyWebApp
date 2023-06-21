using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace KCompanyWebApp.Services;

[DataContract]
public class PricelistServiceList
{
    [DataMember]
    public List<PricelistServiceModel> PricelistServiceModels { get; set; }
}

[DataContract]
public class PricelistServiceModel
{
    [DataMember]
    public string StoreNo { get; set; } = null!;
    [DataMember]
    public string AreaNo { get; set; } = null!;
    [DataMember]
    public string? CustomerType { get; set; }
    [DataMember]
    public DateTime? ValidFrom { get; set; }
    [DataMember]
    public DateTime? ValidTo { get; set; }
    [DataMember]
    public string ProductNo { get; set; } = null!;
    [DataMember]
    public string? ProductDesc { get; set; }
    [DataMember]
    public string? ProductBrand { get; set; }
    [DataMember]
    public string? UoM { get; set; }
    [DataMember]
    public decimal? Amount { get; set; }
}
