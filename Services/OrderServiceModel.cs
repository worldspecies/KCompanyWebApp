using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace KCompanyWebApp.Services;

[DataContract]
public class OrderServiceList
{
    [DataMember]
    public List<OrderServiceModel> OrderServiceModels { get; set; }
    [DataMember]
    public string Message;
}

[DataContract]
public class DetailServiceList
{
    [DataMember]
    public List<DetailServiceModel> DetailServiceModels { get; set; }
    [DataMember]
    public string Message;
}

[DataContract]
public class OrderServiceModel
{
    [DataMember]
    public string OrderNo { get; set; } = null!;
    [DataMember]
    public DateTime? OrderDate { get; set; }
    [DataMember]
    public string? StoreNo { get; set; }
    [DataMember]
    public string? AreaNo { get; set; }
    [DataMember]
    public string? SalesmanNo { get; set; }
    [DataMember]
    public string? CustomerNo { get; set; }
    [DataMember]
    public decimal? GrandTotal { get; set; }
    [DataMember]
    public string Description { get; set; }
}

[DataContract]
public class CreateOrderServiceModel
{
    [DataMember]
    public string? StoreNo { get; set; }
    [DataMember]
    public string? AreaNo { get; set; }
    [DataMember]
    public string? SalesmanNo { get; set; }
    [DataMember]
    public string? CustomerNo { get; set; }
    [DataMember]
    public string Description { get; set; }
}

[DataContract]
public class DetailServiceModel
{
    [DataMember]
    public string OrderNo { get; set; } = null!;
    [DataMember]
    public string? ProductNo { get; set; }
    [DataMember]
    public int? Quantity { get; set; }
    [DataMember]
    public decimal? Price { get; set; }
    [DataMember]
    public decimal? Total { get; set; }
    [DataMember]
    public string Description { get; set; }
}