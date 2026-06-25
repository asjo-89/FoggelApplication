using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Species
{
    public int SpeciesId { get; set; }

    public string SpeciesName { get; set; } = null!;

    public Guid? FileId { get; set; }

    public virtual ICollection<Observation> Observations { get; set; } = new List<Observation>();
}
