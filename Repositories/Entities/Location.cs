using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Location
{
    public int Id { get; set; }

    public string LocationName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Observation> Observations { get; set; } = new List<Observation>();
}
