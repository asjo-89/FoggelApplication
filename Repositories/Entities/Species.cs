using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Entities;

[Index("SpeciesName", Name = "UQ__Species__304D4C0D31EE26C3", IsUnique = true)]
public partial class Species
{
    [Key]
    public int SpeciesId { get; set; }

    [StringLength(100)]
    public string SpeciesName { get; set; } = null!;

    public Guid? FileId { get; set; }

    [InverseProperty("Species")]
    public virtual ICollection<Observation> Observations { get; set; } = new List<Observation>();
}
