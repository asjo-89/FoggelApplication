using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class VwTopListPerMonth
{
    public string MonthName { get; set; } = null!;

    public int ObservationYear { get; set; }

    public int? Antal { get; set; }
}
