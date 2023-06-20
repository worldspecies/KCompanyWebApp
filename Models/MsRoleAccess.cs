using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCompanyWebApp.Models;
[Table("MsRoleAccess")]
public partial class MsRoleAccess
{
    public string Role { get; set; } = null!;

    public string PageNo { get; set; } = null!;
}
