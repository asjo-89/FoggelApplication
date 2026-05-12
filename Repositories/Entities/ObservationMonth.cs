using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Entities;

[Table("ObservationMonth")]
[Index("ObservationYear", "ObservationMonth1", Name = "UQ_ObservationMonth", IsUnique = true)]
public partial class ObservationMonth
{
    [Key]
    public int ObservationMonthId { get; set; }

    public int ObservationYear { get; set; }

    [Column("ObservationMonth")]
    public int ObservationMonth1 { get; set; }

    [InverseProperty("ObservationMonth")]
    public virtual ICollection<Observation> Observations { get; set; } = new List<Observation>();
}
