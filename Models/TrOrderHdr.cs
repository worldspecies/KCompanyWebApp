using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCompanyWebApp.Models;
[Table("TrOrderHdr")]
public partial class TrOrderHdr
{
    public string OrderNo { get; set; } = null!;

    public DateTime? OrderDate { get; set; }

    public string? StoreNo { get; set; }

    public string? AreaNo { get; set; }

    public string? SalesmanNo { get; set; }

    public string? CustomerNo { get; set; }

    public decimal? GrandTotal { get; set; }

    public string? Description { get; set; }

    public string CrtUsrId { get; set; } = null!;

    public DateTime TsCrt { get; set; }

    public string ModUsrId { get; set; } = null!;

    public DateTime TsMod { get; set; }

    public string ActiveFlag { get; set; } = null!;

    public virtual MsCustomer? CustomerNoNavigation { get; set; }

    public virtual MsStore? StoreNoNavigation { get; set; }
}
