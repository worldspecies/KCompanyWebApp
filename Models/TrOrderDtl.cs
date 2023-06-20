using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCompanyWebApp.Models;
[Table("TrOrderDtl")]
public partial class TrOrderDtl
{
    public int OrderDtlID { get; set; }
    public string OrderNo { get; set; } = null!;

    public string? ProductNo { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }
    public string Description { get; set; }

    public string ActiveFlag { get; set; } = null!;

    public virtual TrOrderHdr OrderNoNavigation { get; set; } = null!;

    public virtual MsProductSparepart? ProductNoNavigation { get; set; }
}
