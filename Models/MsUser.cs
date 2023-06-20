using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCompanyWebApp.Models;
[Table("MsUser")]
public partial class MsUser
{
    public string UserId { get; set; } = null!;

    public string? FullName { get; set; }

    public string? AreaNo { get; set; }

    public string? Role { get; set; }

    public string? Password { get; set; }

    public string CrtUsrId { get; set; } = null!;

    public DateTime TsCrt { get; set; }

    public string ModUsrId { get; set; } = null!;

    public DateTime TsMod { get; set; }

    public string ActiveFlag { get; set; } = null!;


}
