using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCompanyWebApp.Models;
[Table("MsStore")]
public partial class MsStore
{
    public string StoreNo { get; set; } = null!;

    public string? StoreDesc { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string AreaNo { get; set; } = null!;

    public string CrtUsrId { get; set; } = null!;

    public DateTime TsCrt { get; set; }

    public string ModUsrId { get; set; } = null!;

    public DateTime TsMod { get; set; }

    public string ActiveFlag { get; set; } = null!;

    public virtual MsMarketingArea AreaNoNavigation { get; set; } = null!;

    public virtual ICollection<TrOrderHdr> TrOrderHdrs { get; set; } = new List<TrOrderHdr>();
}
