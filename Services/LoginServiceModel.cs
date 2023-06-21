using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace KCompanyWebApp.Services;

[DataContract]
public class LoginServiceModel
{
    [DataMember]
    public string? Message { get; set; }
    [DataMember]
    public string UserID { get; set; } = null!;
    public string? AccessToken { get; set; }
    [DataMember]
    public DateTime? Expires { get; set; }
}

