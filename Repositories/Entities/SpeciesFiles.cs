using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Entities
{
    public class SpeciesFiles
    {
        public Guid StreamId { get; set; }
        public byte[] FileData { get; set; } = null!;
        public string? FileName { get; set; } 
        public string? FileType { get; set; } 
    }
}
