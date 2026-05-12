using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Entities;

[Table("Observation")]
[Index("ObservationMonthId", "SpeciesId", Name = "UQ_Observation", IsUnique = true)]
public partial class Observation
{
    [Key]
    public int ObservationId { get; set; }

    public int ObservationMonthId { get; set; }

    public int SpeciesId { get; set; }

    public DateTime? CreatedDate { get; set; }

    [ForeignKey("ObservationMonthId")]
    [InverseProperty("Observations")]
    public virtual ObservationMonth ObservationMonth { get; set; } = null!;

    [ForeignKey("SpeciesId")]
    [InverseProperty("Observations")]
    public virtual Species Species { get; set; } = null!;
}
