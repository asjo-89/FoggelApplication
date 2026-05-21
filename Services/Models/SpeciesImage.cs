using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class SpeciesImage
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Thumbnail64Base { get; set; }
        public string? FileType { get; set; }
    }
}
