using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class SpeciesFilesMetadatum
{
    public int Id { get; set; }

    public int SpeciesId { get; set; }

    public string FileName { get; set; } = null!;

    public string RelativePath { get; set; } = null!;

    public string? FileType { get; set; }

    public DateTime? CreatedAt { get; set; }
}
