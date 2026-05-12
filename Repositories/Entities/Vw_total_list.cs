using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Entities;

[Keyless]
public partial class Vw_total_list
{
    public int ObservationYear { get; set; }

    [StringLength(20)]
    public string MonthName { get; set; } = null!;

    public byte Månadsnummer { get; set; }

    [StringLength(100)]
    public string SpeciesName { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }
}
