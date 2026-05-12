using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Entities;

[Keyless]
public partial class Vw_top_list_per_month
{
    [StringLength(20)]
    public string MonthName { get; set; } = null!;

    public int ObservationYear { get; set; }

    public int? antal { get; set; }
}
