using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCompanyWebApp.Models;
[Table("MsPage")]
public partial class MsPage
{
    public string PageNo { get; set; } = null!;

    public string? Description { get; set; }

    public string PageController { get; set; } = null!;
    public string PageAction { get; set; } = null!;

    public string ActiveFlag { get; set; } = null!;
}
