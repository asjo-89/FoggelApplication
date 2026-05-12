using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Entities;

[Keyless]
public partial class Vw_top_list_per_specy
{
    [StringLength(100)]
    public string SpeciesName { get; set; } = null!;

    public int? ObservationCount { get; set; }
}
