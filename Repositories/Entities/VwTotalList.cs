using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class VwTotalList
{
    public int ObservationYear { get; set; }

    public string MonthName { get; set; } = null!;

    public byte Månadsnummer { get; set; }

    public string SpeciesName { get; set; } = null!;
    public string? LocationName { get; set; }

    public DateTime? CreatedDate { get; set; }
}
