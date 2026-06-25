using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class VwTopListPerSpecy
{
    public string SpeciesName { get; set; } = null!;

    public int? ObservationCount { get; set; }
}
