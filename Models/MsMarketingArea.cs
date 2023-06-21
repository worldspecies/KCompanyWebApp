using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCompanyWebApp.Models;
[Table("MsMarketingArea")]
public partial class MsMarketingArea
{
    public string AreaNo { get; set; } = null!;

    public string? AreaDesc { get; set; }

    public string CrtUsrId { get; set; } = null!;

    public DateTime TsCrt { get; set; }

    public string ModUsrId { get; set; } = null!;

    public DateTime TsMod { get; set; }

    public string ActiveFlag { get; set; } = null!;

    public virtual ICollection<MsStore> MsStores { get; set; } = new List<MsStore>();
}
