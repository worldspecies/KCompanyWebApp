using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace KCompanyWebApp.Services;

[DataContract]
public class CustomerServiceList
{
    [DataMember]
    public List<CustomerServiceModel> CustomerServiceModels { get; set; }
}

[DataContract]
public class CustomerServiceModel
{
    [DataMember]
    public string CustomerNo { get; set; } = null!;
    [DataMember]
    public string? CustomerType { get; set; }
    [DataMember]
    public string? CustomerName { get; set; }
    [DataMember]
    public string? Address { get; set; }
    [DataMember]
    public string? Phone { get; set; }
}
