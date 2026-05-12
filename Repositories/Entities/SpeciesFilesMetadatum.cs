using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Entities;

public partial class SpeciesFilesMetadatum
{
    [Key]
    public int Id { get; set; }

    public int SpeciesId { get; set; }

    [StringLength(50)]
    public string FileName { get; set; } = null!;

    public string RelativePath { get; set; } = null!;

    [StringLength(50)]
    public string? FileType { get; set; }

    public DateTime? CreatedAt { get; set; }
}
