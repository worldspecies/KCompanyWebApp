using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCompanyWebApp.Models;
[Table("MsPricingConfig")]
public partial class MsPricingConfig
{
    public string ProductNo { get; set; } = null!;

    public string StoreNo { get; set; } = null!;

    public string AreaNo { get; set; } = null!;

    public string? CustomerType { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public decimal? Amount { get; set; }

    public string CrtUsrId { get; set; } = null!;

    public DateTime TsCrt { get; set; }

    public string ModUsrId { get; set; } = null!;

    public DateTime TsMod { get; set; }

    public string ActiveFlag { get; set; } = null!;

    public virtual MsProductSparepart ProductNoNavigation { get; set; } = null!;
}
