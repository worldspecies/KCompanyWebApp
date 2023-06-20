using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCompanyWebApp.Models;
[Table("MsProductSparepart")]
public partial class MsProductSparepart
{
    public string ProductNo { get; set; } = null!;

    public string? ProductDesc { get; set; }

    public string? ProductType { get; set; }

    public string? ProductBrand { get; set; }

    public string? UoM { get; set; }

    public decimal? Cogs { get; set; }

    public string CrtUsrId { get; set; } = null!;

    public DateTime TsCrt { get; set; }

    public string ModUsrId { get; set; } = null!;

    public DateTime TsMod { get; set; }

    public string ActiveFlag { get; set; } = null!;
}
