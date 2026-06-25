using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Observation
{
    public int ObservationId { get; set; }

    public int ObservationMonthId { get; set; }

    public int SpeciesId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? LocationId { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ObservationMonth ObservationMonth { get; set; } = null!;

    public virtual Species Species { get; set; } = null!;
}
